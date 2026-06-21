using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Dtos.Payment;
using AMBus.TripManage.Application.Dtos.TicketDto;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Application.Features.BookingsF.Queries.GetTicket;
using AMBus.TripManage.Application.Templates;
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
        private readonly IEmailService _email;

        public ConfirmCashPaymentCommandHandler(
            IUnitOfWork uow, IMapper mapper, ISystemNotificationService notif, IEmailService email)
        { _uow = uow; _mapper = mapper; _notif = notif; _email = email; }
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

            return new PaymentResultDto(true, "تم تأكيد الدفع الكاش بنجاح.", "complete",
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
