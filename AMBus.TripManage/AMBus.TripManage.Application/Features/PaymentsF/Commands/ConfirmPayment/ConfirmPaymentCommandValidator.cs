using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.ConfirmPayment
{
    public class ConfirmPaymentCommandValidator
           : AbstractValidator<ConfirmPaymentCommand>
    {
        public ConfirmPaymentCommandValidator()
        {
            RuleFor(x => x.TransactionId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
