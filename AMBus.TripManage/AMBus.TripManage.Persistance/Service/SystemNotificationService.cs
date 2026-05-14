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
                $"✅ تم تأكيد حجزك | " +
                $"{booking.Trip.Route.FromCity} → {booking.Trip.Route.ToCity} | " +
                $"{booking.Trip.DepartureTime:dd MMM HH:mm} | " +
                $"رمز الحجز: {booking.QrCode}");
        }

        public async Task NotifyBookingCancelledAsync(
            Guid bookingId, Guid cancelledByUserId)
        {
            var booking = await GetBookingWithTripAsync(bookingId);
            if (booking is null) return;

            var msg = $"❌ تم إلغاء حجزك | " +
                      $"{booking.Trip.Route.FromCity} → " +
                      $"{booking.Trip.Route.ToCity}";

            await _sender.SendAsync(
                booking.User.Id, NotificationType.TripCancelled, msg);

            if (cancelledByUserId != booking.UserId)
                await _sender.SendAsync(
                    booking.User.Id,
                    NotificationType.General,
                    "تم إلغاء حجزك من قِبل الإدارة. سيتم استرداد المبلغ خلال 3-5 أيام.");
        }

        public async Task NotifyBookingPendingPaymentAsync(Guid bookingId)
        {
            var booking = await GetBookingWithTripAsync(bookingId);
            if (booking is null) return;

            await _sender.SendAsync(
                booking.User.Id,
                NotificationType.General,
                $"⏳ حجزك في انتظار الدفع | " +
                $"{booking.Trip.Route.FromCity} → {booking.Trip.Route.ToCity} | " +
                $"المبلغ: {booking.TotalPrice} جنيه");
        }

        public async Task NotifyPaymentReceivedAsync(Guid bookingId, decimal amount)
        {
            var booking = await _ctx.Bookings
                .FirstOrDefaultAsync(b => b.Id == bookingId);
            if (booking is null) return;

            await _sender.SendAsync(
                booking.User.Id,
                NotificationType.PaymentReceived,
                $"💰 تم استلام دفعتك بنجاح | المبلغ: {amount} جنيه | " +
                $"رمز الحجز: {booking.QrCode}");
        }

        public async Task NotifyPaymentFailedAsync(Guid bookingId)
        {
            var booking = await _ctx.Bookings
                .FirstOrDefaultAsync(b => b.Id == bookingId);
            if (booking is null) return;

            await _sender.SendAsync(
                booking.User.Id,
                NotificationType.General,
                $"⚠️ فشلت عملية الدفع | رمز الحجز: {booking.QrCode} | " +
                $"يرجى المحاولة مرة أخرى.");
        }

        public async Task NotifyRefundProcessedAsync(Guid bookingId, decimal amount)
        {
            var booking = await _ctx.Bookings
                .FirstOrDefaultAsync(b => b.Id == bookingId);
            if (booking is null) return;

            await _sender.SendAsync(
                booking.User.Id,
                NotificationType.PaymentReceived,
                $"💵 تم إرجاع {amount} جنيه | سيصلك خلال 3-5 أيام عمل.");
        }

        public async Task NotifyTripStartedAsync(Guid tripId)
        {
            var (userIds, msg) = await BuildTripDataAsync(tripId,
                t => $"🚌 رحلتك بدأت | " +
                     $"{t.Route.FromCity} → {t.Route.ToCity} | رحلة موفقة!");

            await _sender.SendBulkAsync(
                userIds, NotificationType.General, msg);
        }

        public async Task NotifyTripCancelledAsync(Guid tripId, string reason)
        {
            var (userIds, msg) = await BuildTripDataAsync(tripId,
                t => $"❌ تم إلغاء الرحلة | " +
                     $"{t.Route.FromCity} → {t.Route.ToCity} | " +
                     $"{t.DepartureTime:dd MMM HH:mm} | " +
                     $"السبب: {reason} | سيتم استرداد المبلغ تلقائياً.");

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
                    $"❌ تم إلغاء رحلتك | {msg}");
        }

        public async Task NotifyTripCompletedAsync(Guid tripId)
        {
            var (userIds, msg) = await BuildTripDataAsync(tripId,
                t => $"✅ اكتملت رحلتك | " +
                     $"{t.Route.FromCity} → {t.Route.ToCity} | " +
                     $"شاركنا تقييمك ⭐");

            await _sender.SendBulkAsync(
                userIds, NotificationType.General, msg);
        }

        public async Task NotifyTripReminderAsync(Guid tripId)
        {
            var (userIds, msg) = await BuildTripDataAsync(tripId,
                t => $"⏰ تذكير | رحلتك بعد ساعة | " +
                     $"{t.Route.FromCity} → {t.Route.ToCity} | " +
                     $"{t.DepartureTime:HH:mm} | كن في موعدك!");

            await _sender.SendBulkAsync(
                userIds, NotificationType.TripReminder, msg);
        }

        // ── Helpers ───────────────────────────────────────

        private async Task<Domain.Entites.Booking?> GetBookingWithTripAsync(Guid bookingId)
            => await _ctx.Bookings
                .Include(b => b.Trip)
                    .ThenInclude(t => t.Route)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

        private async Task<(IEnumerable<Guid> UserIds, string Message)>
            BuildTripDataAsync(
                Guid tripId,
                Func<Domain.Entites.Trip, string> buildMsg)
        {
            var trip = await _ctx.Trips
                .Include(t => t.Route)
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
