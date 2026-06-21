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

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.RefundPayment
{
    public class RefundPaymentCommandHandler
             : IRequestHandler<RefundPaymentCommand, PaymentResultDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymob;
        private readonly ISystemNotificationService _notif;

        public RefundPaymentCommandHandler(
            IUnitOfWork uow, IMapper mapper,
            IPaymentService paymob, ISystemNotificationService notif)
        { _uow = uow; _mapper = mapper; _paymob = paymob; _notif = notif; }

        public async Task<PaymentResultDto> Handle(
            RefundPaymentCommand command, CancellationToken ct)
        {
            var payment = await _uow.Payments
                .GetWithBookingAsync(command.PaymentId)
                ?? throw new NotFoundException(nameof(Payment), command.PaymentId);

            if (payment.Status == PaymentStatus.Refunded)
                throw new ConflictException("تم الاسترداد مسبقاً.");

            if (payment.Status != PaymentStatus.Paid)
                throw new BusinessRuleException("لا يمكن استرداد دفعة غير مكتملة.");

            var booking = payment.Booking;

            if (booking.Status == BookingStatus.Cancelled)
                throw new ConflictException("الحجز ملغي بالفعل.");

            var trip = await _uow.Trips.GetByIdAsync(booking.TripId)
                ?? throw new NotFoundException(nameof(Trip), booking.TripId);

            var timeUntilDeparture = trip.DepartureTime - DateTime.UtcNow;

            if (timeUntilDeparture <= TimeSpan.FromHours(1))
                throw new BusinessRuleException(
                    "لا يمكن استرداد المبلغ خلال أقل من ساعة على انطلاق الرحلة.");

            if (payment.Method != PaymentMethod.Cash)
            {
                // لازم PaymentIntent ID الحقيقي (pi_...) - مش Session ID
                var txId = payment.StripePaymentIntentId
                        ?? throw new BusinessRuleException(
                            "لا يوجد معرف عملية Stripe صالح لهذه الدفعة.");

                var refund = await _paymob.RefundAsync(
                    txId, "Stripe", payment.Amount, command.Reason);

                if (!refund.Success)
                    throw new BusinessRuleException($"فشل الاسترداد: {refund.Error}");
            }

            var now = DateTime.UtcNow;
            var uid = command.AdminId.ToString();
            payment.Status = PaymentStatus.Refunded;
            payment.RefundedAt = now;
            payment.LastModifiedBy = uid;
            payment.LastModifiedDate = now;
            _uow.Payments.Update(payment);

            booking.Status = BookingStatus.Cancelled;
            booking.LastModifiedBy = uid;
            booking.LastModifiedDate = now;

            trip.AvailableSeats += booking.BookingSeats?.Count ?? 0;
            trip.LastModifiedBy = uid;
            trip.LastModifiedDate = now;
            _uow.Trips.Update(trip);

            _uow.Bookings.Update(booking);
            await _uow.SaveChangesAsync();

            await _notif.NotifyRefundProcessedAsync(booking.Id, payment.Amount);

            return new PaymentResultDto(true, "تم الاسترداد بنجاح.", "complete",
                _mapper.Map<PaymentDto>(payment));
        }
    }
    }
