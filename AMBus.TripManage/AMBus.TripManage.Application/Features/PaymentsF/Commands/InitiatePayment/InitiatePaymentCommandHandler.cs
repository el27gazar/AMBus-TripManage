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

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.InitiatePayment
{

    public class InitiatePaymentCommandHandler
        : IRequestHandler<InitiatePaymentCommand, PaymentResultDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymob;

        public InitiatePaymentCommandHandler(
            IUnitOfWork uow, IMapper mapper, IPaymentService paymob)
        { _uow = uow; _mapper = mapper; _paymob = paymob; }

        public async Task<PaymentResultDto> Handle(
            InitiatePaymentCommand command, CancellationToken ct)
        {
            var booking = await _uow.Bookings
                .GetBookingWithDetailsAsync(command.BookingId)
                ?? throw new NotFoundException(nameof(Booking), command.BookingId);

            if (booking.UserId != command.UserId)
                throw new UnauthorizedException("ليس لديك صلاحية.");

            if (booking.Status == BookingStatus.Confirmed)
                throw new ConflictException("الحجز مدفوع بالفعل.");
            if (booking.Status == BookingStatus.Cancelled)
                throw new BusinessRuleException("لا يمكن الدفع لحجز ملغي.");

            var existing = await _uow.Payments.GetByBookingAsync(booking.Id);

            if (existing?.Status == PaymentStatus.Paid)
                throw new ConflictException("الحجز مدفوع بالفعل.");

            if (existing is not null &&
                existing.Status is PaymentStatus.Failed or PaymentStatus.Cancelled)
            {
                _uow.Payments.Delete(existing);
                await _uow.SaveChangesAsync();
                existing = null;
            }

            if (existing?.Status is PaymentStatus.Pending
                                  or PaymentStatus.PendingCustomerAction)
            {
                return new PaymentResultDto(
                    Success: true,
                    Message: "يوجد طلب دفع معلق.",
                    Action: GetAction(existing.Method.ToString()),
                    Payment: _mapper.Map<PaymentDto>(existing));
            }

            if (command.Method == "Cash")
                return await HandleCashAsync(booking, command);

            var user = await _uow.Users.GetByIdAsync(command.UserId)
                ?? throw new NotFoundException(nameof(User), command.UserId);

            var result = await _paymob.InitiatePaymentAsync(new InitiatePaymentRequest(
                BookingId: booking.Id,
                Amount: booking.TotalPrice,
                Currency: command.Currency,
                Method: command.Method,
                PhoneNumber: command.PhoneNumber,
                CustomerName: user.FullName,
                CustomerEmail: user.Email!));

            var method = Enum.Parse<PaymentMethod>(command.Method);
            var isKiosk = command.Method is "Fawry" or "Aman" or "Masary";
            var isWallet = command.Method is "VodafoneCash" or "OrangeCash" or "EtisalatCash";
            var now = DateTime.UtcNow;
            var uid = command.UserId.ToString();

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                BookingId = booking.Id,
                Amount = booking.TotalPrice,
                Currency = command.Currency,
                Method = method,
                Provider = PaymentProvider.Paymob,
                Status = result.Success
                    ? (isKiosk || isWallet
                        ? PaymentStatus.PendingCustomerAction
                        : PaymentStatus.Pending)
                    : PaymentStatus.Failed,
                PaymobOrderId = result.OrderId,
                PaymobTransactionId = result.TransactionId,
                PaymobPaymentToken = result.PaymentToken,
                WalletMsisdn = isWallet ? command.PhoneNumber : null,
                WalletRedirectUrl = result.RedirectUrl,
                FawryReferenceNumber = command.Method == "Fawry"
                                         ? result.ReferenceNumber : null,
                OtcReferenceNumber = isKiosk && command.Method != "Fawry"
                                         ? result.ReferenceNumber : null,
                ExpiresAt = result.ExpiresAt,
                CreatedBy = uid,
                CreatedDate = now,
                LastModifiedBy = uid,
                LastModifiedDate = now
            };

            await _uow.Payments.AddAsync(payment);
            await _uow.SaveChangesAsync();

            return new PaymentResultDto(
                Success: result.Success,
                Message: result.Success
                    ? GetSuccessMessage(command.Method)
                    : $"فشل إنشاء طلب الدفع: {result.Error}",
                Action: result.Success ? GetAction(command.Method) : "error",
                Payment: _mapper.Map<PaymentDto>(payment));
        }

        private async Task<PaymentResultDto> HandleCashAsync(
            Booking booking, InitiatePaymentCommand command)
        {
            var now = DateTime.UtcNow;
            var uid = command.UserId.ToString();
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                BookingId = booking.Id,
                Amount = booking.TotalPrice,
                Currency = command.Currency,
                Method = PaymentMethod.Cash,
                Provider = PaymentProvider.Manual,
                Status = PaymentStatus.PendingCustomerAction,
                CreatedBy = uid,
                CreatedDate = now,
                LastModifiedBy = uid,
                LastModifiedDate = now
            };
            await _uow.Payments.AddAsync(payment);
            await _uow.SaveChangesAsync();

            return new PaymentResultDto(
                Success: true,
                Message: "تم تسجيل طلب الدفع كاش. سيتم التأكيد من قِبل الإدارة.",
                Action: "show_reference",
                Payment: _mapper.Map<PaymentDto>(payment));
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
            "VodafoneCash"
                or "OrangeCash"
                or "EtisalatCash" => "redirect",
            _ => "show_reference"
        };
    }
}

