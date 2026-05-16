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

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.ConfirmCashPayment
{

    public class ConfirmCashPaymentCommandHandler
        : IRequestHandler<ConfirmCashPaymentCommand, PaymentResultDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ISystemNotificationService _notif;

        public ConfirmCashPaymentCommandHandler(
            IUnitOfWork uow, IMapper mapper, ISystemNotificationService notif)
        { _uow = uow; _mapper = mapper; _notif = notif; }

        public async Task<PaymentResultDto> Handle(
            ConfirmCashPaymentCommand command, CancellationToken ct)
        {
            var payment = await _uow.Payments
                .GetWithBookingAsync(command.PaymentId)
                ?? throw new NotFoundException(nameof(Payment), command.PaymentId);

            if (payment.Method != PaymentMethod.Cash)
                throw new BusinessRuleException("هذه الدفعة ليست كاش.");

            if (payment.Status == PaymentStatus.Paid)
                throw new ConflictException("الدفعة مؤكدة بالفعل.");

            if (payment.Status != PaymentStatus.PendingCustomerAction)
                throw new BusinessRuleException("لا يمكن تأكيد هذه الدفعة.");

            var now = DateTime.UtcNow;
            var uid = command.AdminId.ToString();

            payment.Status = PaymentStatus.Paid;
            payment.PaidAt = now;
            payment.LastModifiedBy = uid;
            payment.LastModifiedDate = now;
            _uow.Payments.Update(payment);

            var booking = payment.Booking;
            booking.Status = BookingStatus.Confirmed;
            booking.LastModifiedBy = uid;
            booking.LastModifiedDate = now;
            _uow.Bookings.Update(booking);

            await _uow.SaveChangesAsync();

            await _notif.NotifyPaymentReceivedAsync(booking.Id, payment.Amount);
            await _notif.NotifyBookingConfirmedAsync(booking.Id);

            return new PaymentResultDto(true, "تم تأكيد الدفع الكاش بنجاح.", "complete",
                _mapper.Map<PaymentDto>(payment));
        }
    }
}
