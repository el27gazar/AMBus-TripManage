using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.ConfirmStripePayment
{
    public class ConfirmStripePaymentCommandHandler
         : IRequestHandler<ConfirmStripePaymentCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly ISystemNotificationService _notif;

        public ConfirmStripePaymentCommandHandler(
            IUnitOfWork uow, ISystemNotificationService notif)
        { _uow = uow; _notif = notif; }

        public async Task<Unit> Handle(ConfirmStripePaymentCommand request, CancellationToken ct)
        {
            var payment = await _uow.Payments.GetByExternalTransactionAsync(request.SessionId);

            if (payment is null)
            {
                if (!request.Metadata.TryGetValue("bookingId", out var bookingIdStr))
                    return Unit.Value; 

                var bookingId = Guid.Parse(bookingIdStr);
                payment = await _uow.Payments.GetByBookingAsync(bookingId);
                if (payment is null) return Unit.Value;
            }

            if (payment.Status == PaymentStatus.Paid) return Unit.Value;

            var booking = await _uow.Bookings.GetBookingWithDetailsAsync(payment.BookingId)
                ?? throw new NotFoundException(nameof(Booking), payment.BookingId);

            var now = DateTime.UtcNow;

            payment.Status = PaymentStatus.Paid;
            payment.PaidAt = now;
            payment.ExternalTransactionId = request.SessionId;
            payment.StripePaymentIntentId = request.PaymentIntentId;
            payment.LastModifiedDate = now;
            _uow.Payments.Update(payment);

            booking.Status = BookingStatus.Confirmed;
            booking.LastModifiedDate = now;
            _uow.Bookings.Update(booking);

            await _uow.SaveChangesAsync();

            await _notif.NotifyPaymentReceivedAsync(booking.Id, payment.Amount);
            await _notif.NotifyBookingConfirmedAsync(booking.Id);

            return Unit.Value;
        }
    }
  }
