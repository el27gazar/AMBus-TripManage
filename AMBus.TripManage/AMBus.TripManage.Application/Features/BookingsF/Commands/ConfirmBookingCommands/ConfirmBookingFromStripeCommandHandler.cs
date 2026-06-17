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

            var tripId = Guid.Parse(request.Metadata["tripId"]);
            var userId = Guid.Parse(request.Metadata["userId"]);
            var seatIds = request.Metadata["seatIds"].Split(',').Select(Guid.Parse).ToList();
            var totalPrice = decimal.Parse(request.Metadata["totalPrice"]);
            var passengerName = request.Metadata["passengerName"];
            var passengerIdNumber = request.Metadata["passengerIdNumber"];

            // ✅ التحقق الحاسم - هل المقاعد لسه فاضية فعلاً وقت الدفع؟
            foreach (var seatId in seatIds)
            {
                var taken = await _uow.Bookings.IsSeatAlreadyBookedAsync(seatId, tripId);
                if (taken)
                {
                    // المقعد اتحجز من حد تاني في نفس اللحظة - لازم استرداد فوري
                    // (يُنصأ بإضافة منطق Refund هنا تلقائياً)
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
                Status = BookingStatus.Confirmed,   // ✅ مؤكد مباشرة لأن الدفع نجح فعلاً
                TotalPrice = totalPrice,
                BookedAt = now,
                QrCode = Guid.NewGuid().ToString("N")[..12].ToUpper(),
                BookingSeats = seatIds.Select(seatId => new BookingSeat
                {
                    Id = Guid.NewGuid(),
                    SeatId = seatId,
                    PassengerName = passengerName,
                    PassengerIdNumber = passengerIdNumber,
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