using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Booking;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.ConfirmBookingCommands
{

    public class ConfirmBookingFromStripeCommandHandler
         : IRequestHandler<ConfirmBookingFromStripeCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public ConfirmBookingFromStripeCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Unit> Handle(ConfirmBookingFromStripeCommand request, CancellationToken ct)
        {
            // منع التكرار - لو الـ session دي بالفعل تم إنشاء حجز لها
            var existingPayment = await _uow.Payments.GetByExternalTransactionAsync(request.SessionId);
            if (existingPayment != null) return Unit.Value;

            // قراءة آمنة للـ Metadata بدل الـ indexer المباشر
            if (!request.Metadata.TryGetValue("tripId", out var tripIdStr) ||
                !request.Metadata.TryGetValue("userId", out var userIdStr) ||
                !request.Metadata.TryGetValue("seatIds", out var seatIdsStr) ||
                !request.Metadata.TryGetValue("totalPrice", out var totalPriceStr) ||
                !request.Metadata.TryGetValue("passengerName", out var passengerName))
            {
                throw new BusinessRuleException(
                    "بيانات الحجز ناقصة في الـ Metadata القادمة من Stripe Webhook.");
            }

            request.Metadata.TryGetValue("phoneNumber", out var phoneNumber);

            var tripId = Guid.Parse(tripIdStr);
            var userId = Guid.Parse(userIdStr);
            var seatIds = seatIdsStr.Split(',').Select(Guid.Parse).ToList();
            var totalPrice = decimal.Parse(totalPriceStr, CultureInfo.InvariantCulture);

            foreach (var seatId in seatIds)
            {
                var taken = await _uow.Bookings.IsSeatAlreadyBookedAsync(seatId, tripId);
                if (taken)
                {
                    throw new ConflictException("تم حجز أحد المقاعد بالفعل من مستخدم آخر، سيتم استرداد المبلغ.");
                }
            }

            var trip = await _uow.Trips.GetByIdAsync(tripId)
                ?? throw new NotFoundException(nameof(Trip), tripId);

            var now = DateTime.UtcNow;
            var uid = userId.ToString();

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                TripId = tripId,
                UserId = userId,
                Status = BookingStatus.Confirmed,
                TotalPrice = totalPrice,
                BookedAt = now,
                QrCode = Guid.NewGuid().ToString("N")[..12].ToUpper(),
                BookingSeats = seatIds.Select(seatId => new BookingSeat
                {
                    Id = Guid.NewGuid(),
                    SeatId = seatId,
                    PassengerName = passengerName,
                    CreatedDate = now
                }).ToList(),
                CreatedBy = uid,
                CreatedDate = now,
                LastModifiedBy = uid,
                LastModifiedDate = now
            };

            await _uow.Bookings.AddAsync(booking);

            trip.AvailableSeats -= seatIds.Count;
            _uow.Trips.Update(trip);

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                BookingId = booking.Id,
                Amount = totalPrice,
                Currency = "EGP",
                Method = PaymentMethod.Card,
                Provider = PaymentProvider.Stripe,
                Status = PaymentStatus.Paid,
                PaidAt = now,
                ExternalTransactionId = request.SessionId,
                StripePaymentIntentId = request.PaymentIntentId,
                CreatedBy = uid,
                CreatedDate = now,
                LastModifiedBy = uid,
                LastModifiedDate = now
            };

            await _uow.Payments.AddAsync(payment);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }

}