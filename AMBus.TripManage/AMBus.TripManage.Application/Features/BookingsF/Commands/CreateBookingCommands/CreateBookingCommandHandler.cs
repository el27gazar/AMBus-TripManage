using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.BookingDto;
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
using PaymentDto = AMBus.TripManage.Application.Dtos.Payment.PaymentDto;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.CreateBookingCommands
{
    public class CreateBookingCommandHandler
       : IRequestHandler<CreateBookingCommand, BookingWithPaymentDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ISystemNotificationService _notifications;
        private readonly IPaymentService _paymob; // أضف ده

        public CreateBookingCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ISystemNotificationService notifications,
            IPaymentService paymob) // أضف ده
        {
            _uow = uow;
            _mapper = mapper;
            _notifications = notifications;
            _paymob = paymob;
        }

        public async Task<BookingWithPaymentDto> Handle(
            CreateBookingCommand request,
            CancellationToken cancellationToken)
        {
            // ── نفس الكود الموجود بالظبط ──
            var trip = await _uow.Trips.GetTripWithDetailsAsync(request.TripId)
                ?? throw new NotFoundException(nameof(Trip), request.TripId);

            if (trip.Status != TripStatus.Scheduled)
                throw new BusinessRuleException("Unavailable Trip");

            if (trip.AvailableSeats < request.Seats.Count)
                throw new BusinessRuleException(
                    $"Available seats ({trip.AvailableSeats}) are less than requested.");

            foreach (var s in request.Seats)
            {
                var taken = await _uow.Bookings
                    .IsSeatAlreadyBookedAsync(s.SeatId, request.TripId);
                if (taken)
                    throw new ConflictException("One of the seats is already booked.");
            }

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                TripId = request.TripId,
                TotalPrice = trip.BasePrice * request.Seats.Count,
                Status = BookingStatus.Pending,
                CreatedDate = DateTime.UtcNow,
                QrCode = Guid.NewGuid().ToString("N")[..12].ToUpper(),
                BookingSeats = request.Seats.Select(s => new BookingSeat
                {
                    Id = Guid.NewGuid(),
                    SeatId = s.SeatId,
                    PassengerName = s.PassengerName,
                    PassengerIdNumber = s.PassengerIdNumber,
                    CreatedDate = DateTime.UtcNow
                }).ToList()
            };

            trip.AvailableSeats -= request.Seats.Count;
            trip.LastModifiedDate = DateTime.UtcNow;
            _uow.Trips.Update(trip);
            await _uow.Bookings.AddAsync(booking);
            await _uow.SaveChangesAsync();

            await _notifications.NotifyBookingPendingPaymentAsync(booking.Id);

            var created = await _uow.Bookings
                .GetBookingWithDetailsAsync(booking.Id) ?? booking;

            // ── الجديد: ابدأ الـ payment تلقائياً ──
            var user = await _uow.Users.GetByIdAsync(request.UserId)
                ?? throw new NotFoundException(nameof(User), request.UserId);

            var paymobResult = await _paymob.InitiatePaymentAsync(
                new InitiatePaymentRequest(
                    BookingId: booking.Id,
                    Amount: booking.TotalPrice,
                    Currency: "EGP",
                    Method: request.PaymentMethod,        // "Card" / "VodafoneCash" / etc
                    PhoneNumber: request.PhoneNumber,
                    CustomerName: user.FullName,
                    CustomerEmail: user.Email!));

            var method = Enum.Parse<PaymentMethod>(request.PaymentMethod);
            var isKiosk = request.PaymentMethod is "Fawry" or "Aman" or "Masary";
            var isWallet = request.PaymentMethod is "VodafoneCash" or "OrangeCash" or "EtisalatCash";
            var now = DateTime.UtcNow;
            var uid = request.UserId.ToString();

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                BookingId = booking.Id,
                Amount = booking.TotalPrice,
                Currency = "EGP",
                Method = method,
                Provider = PaymentProvider.Paymob,
                Status = paymobResult.Success
                    ? (isKiosk || isWallet
                        ? PaymentStatus.PendingCustomerAction
                        : PaymentStatus.Pending)
                    : PaymentStatus.Failed,
                PaymobOrderId = paymobResult.OrderId,
                PaymobTransactionId = paymobResult.TransactionId,
                PaymobPaymentToken = paymobResult.PaymentToken,
                WalletMsisdn = isWallet ? request.PhoneNumber : null,
                WalletRedirectUrl = paymobResult.RedirectUrl,
                FawryReferenceNumber = request.PaymentMethod == "Fawry"
                                         ? paymobResult.ReferenceNumber : null,
                OtcReferenceNumber = isKiosk && request.PaymentMethod != "Fawry"
                                         ? paymobResult.ReferenceNumber : null,
                ExpiresAt = paymobResult.ExpiresAt,
                CreatedBy = uid,
                CreatedDate = now,
                LastModifiedBy = uid,
                LastModifiedDate = now
            };

            await _uow.Payments.AddAsync(payment);
            await _uow.SaveChangesAsync();

            var paymentResultDto = new PaymentResultDto(
                Success: paymobResult.Success,
                Message: paymobResult.Success
                    ? GetSuccessMessage(request.PaymentMethod)
                    : $"تم الحجز لكن فشل الدفع: {paymobResult.Error}",
                Action: paymobResult.Success
                    ? GetAction(request.PaymentMethod) : "error",
                Payment: _mapper.Map<PaymentDto>(payment));

            return new BookingWithPaymentDto(
                Booking: _mapper.Map<BookingDto>(created),
                Payment: paymentResultDto);
        }

        private static string GetSuccessMessage(string method) => method switch
        {
            "Card" => "أكمل الدفع بالبطاقة.",
            "VodafoneCash" => "تم إرسال طلب Vodafone Cash. أكمل التأكيد.",
            "OrangeCash" => "تم إرسال طلب Orange Cash. أكمل التأكيد.",
            "EtisalatCash" => "تم إرسال طلب Etisalat Cash. أكمل التأكيد.",
            "Fawry" => "ادفع برقم الفاتورة عند أي كشك Fawry خلال 48 ساعة.",
            "Aman" => "ادفع بالرقم المرجعي عند أي فرع Aman خلال 24 ساعة.",
            "Masary" => "ادفع بالرقم المرجعي عند أي محطة Masary خلال 24 ساعة.",
            _ => "تم إنشاء طلب الدفع."
        };

        private static string GetAction(string method) => method switch
        {
            "Card" => "iframe",
            "VodafoneCash" or "OrangeCash" or "EtisalatCash" => "redirect",
            _ => "show_reference"
        };
    }
}
