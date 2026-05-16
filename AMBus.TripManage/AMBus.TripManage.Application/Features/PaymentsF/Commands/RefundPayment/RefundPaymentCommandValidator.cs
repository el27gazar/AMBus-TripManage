using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.RefundPayment
{
    public class RefundPaymentCommandValidator
         : AbstractValidator<RefundPaymentCommand>
    {
        public RefundPaymentCommandValidator()
        {
            RuleFor(x => x.PaymentId).NotEmpty();
            RuleFor(x => x.AdminId).NotEmpty();
            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("سبب الاسترداد مطلوب.")
                .MaximumLength(200);
        }
    }
}
