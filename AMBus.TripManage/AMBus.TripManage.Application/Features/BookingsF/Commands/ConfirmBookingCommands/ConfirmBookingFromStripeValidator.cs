using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.ConfirmBookingCommands
{
    public class ConfirmBookingFromStripeValidator:AbstractValidator<ConfirmBookingFromStripeCommand>
    {
        public ConfirmBookingFromStripeValidator()
        {
            RuleFor(x=>x.SessionId).NotEmpty();
            RuleFor(x=>x.Metadata).NotEmpty();
            RuleFor(x=>x.PaymentIntentId).NotEmpty();

        }
    }
}
