using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.CancelPendingPayment
{
    public class CancelPendingPaymentCommandHandler
            : IRequestHandler<CancelPendingPaymentCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public CancelPendingPaymentCommandHandler(IUnitOfWork uow)
            => _uow = uow;

        public async Task<Unit> Handle(
            CancelPendingPaymentCommand command, CancellationToken ct)
        {
            var booking = await _uow.Bookings.GetByIdAsync(command.BookingId)
                ?? throw new NotFoundException(nameof(Booking), command.BookingId);

            if (booking.UserId != command.UserId)
                throw new UnauthorizedException("ليس لديك صلاحية.");

            var payment = await _uow.Payments.GetByBookingAsync(command.BookingId)
                ?? throw new NotFoundException("لا توجد دفعة لهذا الحجز.", command.BookingId);

            if (payment.Status is not PaymentStatus.Pending
                               and not PaymentStatus.PendingCustomerAction)
                throw new BusinessRuleException("لا يمكن إلغاء إلا الدفعات المعلقة.");

            payment.Status = PaymentStatus.Cancelled;
            payment.LastModifiedBy = command.UserId.ToString();
            payment.LastModifiedDate = DateTime.UtcNow;
            _uow.Payments.Update(payment);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
