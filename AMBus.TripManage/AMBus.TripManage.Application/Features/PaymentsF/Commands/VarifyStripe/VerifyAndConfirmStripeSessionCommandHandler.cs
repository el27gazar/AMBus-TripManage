using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Dtos.Payment;
using AMBus.TripManage.Application.Dtos.TicketDto;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Application.Templates;
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
        private readonly IEmailService _email;

        public VerifyAndConfirmStripeSessionCommandHandler(
            IUnitOfWork uow, IMapper mapper,
            IPaymentService paymentService, ISystemNotificationService notif, IEmailService email)
        { _uow = uow; _mapper = mapper; _paymentService = paymentService; _notif = notif; _email = email; }

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

            try
            {
                var fullBooking = await _uow.Bookings.GetBookingWithDetailsAsync(booking.Id);
                if (fullBooking != null)
                {
                    var ticket = _mapper.Map<TicketDto>(fullBooking);
                    await SendTicketEmailAsync(fullBooking, payment, ticket);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Failed to send ticket email: {ex.Message}");
            }

            return new PaymentResultDto(true, "تم تأكيد الدفع بنجاح.", "complete",
                _mapper.Map<PaymentDto>(payment));
        }


        private async Task SendTicketEmailAsync(Booking booking, Payment payment, TicketDto ticket)
        {
            if (string.IsNullOrWhiteSpace(booking.User?.Email))
                return;

            var seatNumbers = ticket.Passengers?.Select(p => p.SeatNumber).ToList()
                ?? new List<string>();

            var subject = "تذكرتك جاهزة - AMBus";

            var body = EmailTemplates.Ticket(
                fullName: booking.User.FullName,
                fromCity: ticket.FromCity,
                toCity: ticket.ToCity,
                departureTime: ticket.DepartureTime,
                busPlate: ticket.BusPlate,
                busType: ticket.BusType,
                seatNumbers: seatNumbers,
                qrCodeBase64: ticket.QrCode,
                bookingId: ticket.BookingId);

            await _email.SendEmailAsync(booking.User.Email, subject, body);
        }
    }

}