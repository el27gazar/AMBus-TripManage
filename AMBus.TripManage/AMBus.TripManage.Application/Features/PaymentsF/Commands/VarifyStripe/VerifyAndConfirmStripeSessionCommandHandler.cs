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

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.VarifyStripe
{
    public class VerifyAndConfirmStripeSessionCommandHandler
        : IRequestHandler<VerifyAndConfirmStripeSessionCommand, PaymentResultDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;
        private readonly ISystemNotificationService _notif;

        public VerifyAndConfirmStripeSessionCommandHandler(
            IUnitOfWork uow, IMapper mapper,
            IPaymentService paymentService, ISystemNotificationService notif)
        { _uow = uow; _mapper = mapper; _paymentService = paymentService; _notif = notif; }

        public async Task<PaymentResultDto> Handle(
            VerifyAndConfirmStripeSessionCommand request, CancellationToken ct)
        {
            var payment = await _uow.Payments.GetByExternalTransactionAsync(request.SessionId)
                ?? throw new NotFoundException("Payment", request.SessionId);

            var booking = await _uow.Bookings.GetBookingWithDetailsAsync(payment.BookingId)
                ?? throw new NotFoundException(nameof(Booking), payment.BookingId);

            if (booking.UserId != request.UserId)
                throw new UnauthorizedException("ليس لديك صلاحية على هذا الحجز.");

            if (payment.Status == PaymentStatus.Paid)
            {
                return new PaymentResultDto(true, "الدفعة مؤكدة بالفعل.", "complete",
                    _mapper.Map<PaymentDto>(payment));
            }

            var verify = await _paymentService.VerifyCheckoutSessionAsync(request.SessionId);

            if (!verify.Success)
            {
                return new PaymentResultDto(false,
                    $"تعذّر التحقق من حالة الدفع: {verify.Error}", "error",
                    _mapper.Map<PaymentDto>(payment));
            }

            if (verify.Status != "paid")
            {
                // لسه مدفوعة، أو العميل لغى، أو الجلسة منتهية - ما نلمسش الداتا بيز
                return new PaymentResultDto(false,
                    "لم يكتمل الدفع بعد.", "pending",
                    _mapper.Map<PaymentDto>(payment));
            }

            // 5) Stripe أكدت الدفع فعليًا - دلوقتي بس نحدّث الداتا بيز
            var now = DateTime.UtcNow;

            payment.Status = PaymentStatus.Paid;
            payment.PaidAt = now;
            payment.StripePaymentIntentId = verify.PaymentIntentId;
            payment.LastModifiedDate = now;
            _uow.Payments.Update(payment);

            booking.Status = BookingStatus.Confirmed;
            booking.LastModifiedDate = now;
            _uow.Bookings.Update(booking);

            await _uow.SaveChangesAsync();

            await _notif.NotifyPaymentReceivedAsync(booking.Id, payment.Amount);
            await _notif.NotifyBookingConfirmedAsync(booking.Id);

            return new PaymentResultDto(true, "تم تأكيد الدفع بنجاح.", "complete",
                _mapper.Map<PaymentDto>(payment));
        }
    }
}