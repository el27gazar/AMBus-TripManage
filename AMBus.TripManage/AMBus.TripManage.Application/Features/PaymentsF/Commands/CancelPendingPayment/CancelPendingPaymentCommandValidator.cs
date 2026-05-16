using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.CancelPendingPayment
{
    public class CancelPendingPaymentCommandValidator
           : AbstractValidator<CancelPendingPaymentCommand>
    {
        public CancelPendingPaymentCommandValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
