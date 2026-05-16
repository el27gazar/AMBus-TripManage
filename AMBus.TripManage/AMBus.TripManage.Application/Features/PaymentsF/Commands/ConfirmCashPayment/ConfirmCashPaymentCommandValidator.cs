using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.ConfirmCashPayment
{
    public class ConfirmCashPaymentCommandValidator
           : AbstractValidator<ConfirmCashPaymentCommand>
    {
        public ConfirmCashPaymentCommandValidator()
        {
            RuleFor(x => x.PaymentId).NotEmpty();
            RuleFor(x => x.AdminId).NotEmpty();
        }
    }
}
