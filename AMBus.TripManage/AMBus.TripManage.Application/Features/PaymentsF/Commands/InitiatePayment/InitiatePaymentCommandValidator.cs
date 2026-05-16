using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.InitiatePayment
{
    public class InitiatePaymentCommandValidator
           : AbstractValidator<InitiatePaymentCommand>
    {
        private static readonly string[] AllowedMethods =
        {
            "Card","VodafoneCash","OrangeCash","EtisalatCash",
            "Fawry","Aman","Masary","Cash"
        };

        private static readonly string[] RequirePhone =
        {
            "VodafoneCash","OrangeCash","EtisalatCash",
            "Fawry","Aman","Masary"
        };

        public InitiatePaymentCommandValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Method)
                .NotEmpty()
                .Must(m => AllowedMethods.Contains(m))
                .WithMessage($"طريقة الدفع يجب أن تكون: {string.Join(", ", AllowedMethods)}.");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("رقم الموبايل مطلوب.")
                .Matches(@"^(010|011|012|015)\d{8}$").WithMessage("رقم الموبايل غير صحيح.")
                .When(x => RequirePhone.Contains(x.Method));
        }
    }
}
