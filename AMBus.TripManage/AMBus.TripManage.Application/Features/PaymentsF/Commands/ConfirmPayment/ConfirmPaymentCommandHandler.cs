using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Dtos.Payment;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.ConfirmPayment
{
    public class ConfirmPaymentCommandHandler
           : IRequestHandler<ConfirmPaymentCommand, PaymentResultDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymob;
        private readonly ISystemNotificationService _notif;

        public ConfirmPaymentCommandHandler(
            IUnitOfWork uow, IMapper mapper,
            IPaymentService paymob, ISystemNotificationService notif)
        { _uow = uow; _mapper = mapper; _paymob = paymob; _notif = notif; }

        public async Task<PaymentResultDto> Handle(
            ConfirmPaymentCommand command, CancellationToken ct)
        {
            var payment = await _uow.Payments
                .GetByTransactionIdAsync(command.TransactionId)
                ?? throw new NotFoundException("Transaction", command.TransactionId);

            var booking = await _uow.Bookings
                .GetBookingWithDetailsAsync(payment.BookingId)
                ?? throw new NotFoundException(nameof(Booking), payment.BookingId);

            if (booking.UserId != command.UserId)
                throw new UnauthorizedException("ليس لديك صلاحية.");

            if (payment.Status == PaymentStatus.Paid)
                throw new ConflictException("الدفعة مؤكدة بالفعل.");

            var verify = await _paymob.VerifyPaymentAsync(
                command.TransactionId, "Paymob");

            var now = DateTime.UtcNow;
            var uid = command.UserId.ToString();

            if (verify.Success && verify.Status == "succeeded")
            {
                payment.Status = PaymentStatus.Paid;
                payment.StripeClientSecret = command.TransactionId;
                payment.ExternalTransactionId = command.TransactionId;
                payment.PaidAt = now;
                payment.LastModifiedBy = uid;
                payment.LastModifiedDate = now;
                _uow.Payments.Update(payment);

                booking.Status = BookingStatus.Confirmed;
                booking.LastModifiedBy = uid;
                booking.LastModifiedDate = now;
                _uow.Bookings.Update(booking);

                await _uow.SaveChangesAsync();

                await _notif.NotifyPaymentReceivedAsync(booking.Id, payment.Amount);
                await _notif.NotifyBookingConfirmedAsync(booking.Id);

                return new PaymentResultDto(true, "تم تأكيد الدفع بنجاح.", "complete",
                    _mapper.Map<PaymentDto>(payment));
            }
            else
            {
                payment.Status = PaymentStatus.Failed;
                payment.LastModifiedBy = uid;
                payment.LastModifiedDate = now;
                _uow.Payments.Update(payment);
                await _uow.SaveChangesAsync();

                await _notif.NotifyPaymentFailedAsync(booking.Id);

                return new PaymentResultDto(
                    false, $"فشل الدفع: {verify.Error ?? verify.Status}",
                    "error", _mapper.Map<PaymentDto>(payment));
            }
        }
    }

}
