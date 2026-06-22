using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Domain.Entites;
using AMBus.TripManage.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Service
{
    public class SystemNotificationService : ISystemNotificationService
    {
        private readonly AppDbContext _ctx;
        private readonly INotificationSender _sender;

        public SystemNotificationService(
            AppDbContext ctx, INotificationSender sender)
        {
            _ctx = ctx;
            _sender = sender;
        }

        public async Task NotifyBookingConfirmedAsync(Guid bookingId)
        {
            var booking = await GetBookingWithTripAsync(bookingId);
            if (booking is null) return;

            await _sender.SendAsync(
                booking.User.Id,
                NotificationType.BookingConfirmed,
                $"✅ Your reservation has been confirmed | " +
                $"{booking.Trip.From.Name} → {booking.Trip.To.Name} | " +
                $"{booking.Trip.DepartureTime:dd MMM HH:mm} | " +
                $"Reservation Code: {booking.QrCode}");
        }

        public async Task NotifyBookingCancelledAsync(
            Guid bookingId, Guid cancelledByUserId)
        {
            var booking = await GetBookingWithTripAsync(bookingId);
            if (booking is null) return;

            var msg = $"❌ Your reservation has been cancelled | " +
                      $"{booking.Trip.From.Name} → " +
                      $"{booking.Trip.To.Name}";

            await _sender.SendAsync(
                booking.User.Id, NotificationType.TripCancelled, msg);

            if (cancelledByUserId != booking.UserId)
                await _sender.SendAsync(
                    booking.User.Id,
                    NotificationType.General,
                    "Your booking has been cancelled by management. You will receive a refund within 3-5 days.");
        }

        public async Task NotifyBookingPendingPaymentAsync(Guid bookingId)
        {
            var booking = await GetBookingWithTripAsync(bookingId);
            if (booking is null) return;

            await _sender.SendAsync(
                booking.User.Id,
                NotificationType.General,
                $"⏳ Your reservation is pending payment | " +
                $"{booking.Trip.From.Name} → {booking.Trip.To.Name} | " +
                $"Price: {booking.TotalPrice} EGP");
        }

        public async Task NotifyPaymentReceivedAsync(Guid bookingId, decimal amount)
        {
            var booking = await _ctx.Bookings
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
            if (booking is null) return;

            await _sender.SendAsync(
                booking.User.Id,
                NotificationType.PaymentReceived,
                $"💰 Your payment has been successfully received | Amount: {amount} EGP | " +
                $"Reservation Code: {booking.QrCode}");
        }

        public async Task NotifyPaymentFailedAsync(Guid bookingId)
        {
            var booking = await _ctx.Bookings
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
            if (booking is null) return;

            await _sender.SendAsync(
                booking.User.Id,
                NotificationType.General,
                $"⚠️ Payment failed | Booking code: {booking.QrCode} | " +
                $"Try again later.");
        }

        public async Task NotifyRefundProcessedAsync(Guid bookingId, decimal amount)
        {
            var booking = await _ctx.Bookings
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking is null) return;

            await _sender.SendAsync(
                booking.User.Id,
                NotificationType.PaymentReceived,
                $"Thank you | You will receive it within 3-5 business days {amount} EGP | Returned");
        }

        public async Task NotifyTripStartedAsync(Guid tripId)
        {
            var (userIds, msg) = await BuildTripDataAsync(tripId,
                t => $"🚌Your journey has begun | " +
                     $"{t.From.Name} → {t.To.Name} | god speed!");

            await _sender.SendBulkAsync(
                userIds, NotificationType.General, msg);
        }

        public async Task NotifyTripCancelledAsync(Guid tripId, string reason)
        {
            var (userIds, msg) = await BuildTripDataAsync(tripId,
                t => $"❌The flight has been cancelled | " +
                     $"{t.From.Name} → {t.To.Name} | " +
                     $"{t.DepartureTime:dd MMM HH:mm} | " +
                     $"Reason: {reason} | The amount will be refunded automatically.");

            await _sender.SendBulkAsync(
                userIds, NotificationType.TripCancelled, msg);

            // إشعار السائق
            var trip = await _ctx.Trips
                .Include(t => t.Driver)
                .FirstOrDefaultAsync(t => t.Id == tripId);

            if (trip is not null)
                await _sender.SendAsync(
                    trip.Driver.UserId,
                    NotificationType.TripCancelled,
                    $"❌ Your flight has been cancelled| {msg}");
        }

        public async Task NotifyTripCompletedAsync(Guid tripId)
        {
            var (userIds, msg) = await BuildTripDataAsync(tripId,
                t => $"✅Your journey is complete | " +
                     $"{t.From.Name} → {t.To.Name} | " +
                     $"Share your rating with us ⭐");

            await _sender.SendBulkAsync(
                userIds, NotificationType.General, msg);
        }

        public async Task NotifyTripReminderAsync(Guid tripId)
        {
            var (userIds, msg) = await BuildTripDataAsync(tripId,
                t => $"⏰Reminder | Your flight is in one hour | " +
                     $"{t.From.Name} → {t.To.Name} | " +
                     $"{t.DepartureTime:HH:mm} | Be on time!");

            await _sender.SendBulkAsync(
                userIds, NotificationType.TripReminder, msg);
        }

        // ── Helpers ───────────────────────────────────────

        private async Task<Domain.Entites.Booking?> GetBookingWithTripAsync(Guid bookingId)
            => await _ctx.Bookings
                .Include(b => b.User)
                .Include(b => b.Trip)
                    .ThenInclude(t => t.From)
                .Include(b => b.Trip)
                    .ThenInclude(t => t.To)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

        private async Task<(IEnumerable<Guid> UserIds, string Message)>
            BuildTripDataAsync(
                Guid tripId,
                Func<Domain.Entites.Trip, string> buildMsg)
        {
            var trip = await _ctx.Trips
                .Include(t => t.From)
                .Include(t => t.To)
                .FirstOrDefaultAsync(t => t.Id == tripId);

            if (trip is null)
                return (Enumerable.Empty<Guid>(), string.Empty);

            var userIds = await _ctx.Bookings
                .Where(b =>
                    b.TripId == tripId &&
                    b.Status != BookingStatus.Cancelled)
                .Select(b => b.UserId)
                .Distinct()
                .ToListAsync();

            return (userIds, buildMsg(trip));
        }
    }
}
