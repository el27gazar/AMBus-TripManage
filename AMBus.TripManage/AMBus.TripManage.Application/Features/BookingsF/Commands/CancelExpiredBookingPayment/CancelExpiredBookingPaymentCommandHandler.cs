using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.CancelExpiredBookingPayment
{
    public class CancelExpiredBookingPaymentCommandHandler
           : IRequestHandler<CancelExpiredBookingPaymentCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public CancelExpiredBookingPaymentCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Unit> Handle(CancelExpiredBookingPaymentCommand request, CancellationToken ct)
        {
            var payment = await _uow.Payments.GetByExternalTransactionAsync(request.SessionId);
            if (payment is null) return Unit.Value;

            if (payment.Status == PaymentStatus.Paid) return Unit.Value;

            var booking = await _uow.Bookings.GetBookingWithDetailsAsync(payment.BookingId);
            if (booking is null || booking.Status == BookingStatus.Cancelled)
                return Unit.Value;

            var now = DateTime.UtcNow;

            payment.Status = PaymentStatus.Failed;
            payment.LastModifiedDate = now;
            _uow.Payments.Update(payment);

            booking.Status = BookingStatus.Cancelled;
            booking.LastModifiedDate = now;
            _uow.Bookings.Update(booking);

            var trip = await _uow.Trips.GetByIdAsync(booking.TripId);
            if (trip != null)
            {
                trip.AvailableSeats += booking.BookingSeats.Count;
                _uow.Trips.Update(trip);
            }

            await _uow.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
