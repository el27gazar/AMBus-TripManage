using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.CreateBookingCommands
{
    public class InitiateBookingPaymentCommandValidator
         : AbstractValidator<InitiateBookingPaymentCommand>
    {
        private static readonly string[] AllowedPaymentMethods =
        {
            "Cash", "Card", "VodafoneCash", "OrangeCash",
            "EtisalatCash", "Fawry", "Aman", "Masary"
        };

        private static readonly string[] WalletMethods =
        {
            "VodafoneCash", "OrangeCash", "EtisalatCash"
        };

        public InitiateBookingPaymentCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("معرّف المستخدم مطلوب.");

            RuleFor(x => x.TripId)
                .NotEmpty().WithMessage("معرّف الرحلة مطلوب.");

            RuleFor(x => x.Seats)
                .NotNull().WithMessage("يجب اختيار مقعد واحد على الأقل.")
                .NotEmpty().WithMessage("يجب اختيار مقعد واحد على الأقل.");

            RuleForEach(x => x.Seats)
                .ChildRules(seat =>
                {
                    seat.RuleFor(s => s.SeatId)
                        .NotEmpty().WithMessage("معرّف المقعد غير صالح.");
                })
                .When(x => x.Seats is { Count: > 0 });

            RuleFor(x => x.Seats)
                .Must(seats => seats.Select(s => s.SeatId).Distinct().Count() == seats.Count)
                .WithMessage("لا يمكن اختيار نفس المقعد أكثر من مرة.")
                .When(x => x.Seats is { Count: > 0 });

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("طريقة الدفع مطلوبة.")
                .Must(method => AllowedPaymentMethods.Contains(method))
                .WithMessage("طريقة الدفع غير صالحة. الطرق المتاحة: "
                    + string.Join(", ", AllowedPaymentMethods));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("رقم الهاتف مطلوب لطريقة الدفع المحددة.")
                .Matches(@"^01[0125][0-9]{8}$")
                    .WithMessage("رقم الهاتف غير صالح، يجب إدخال رقم مصري صحيح.")
                .When(x => WalletMethods.Contains(x.PaymentMethod));
        }
    }
}
