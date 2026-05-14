using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.CancelBooking
{
    public class CancelBookingCommandValidator:AbstractValidator<CancelBookingCommand>
    {
        public CancelBookingCommandValidator()
        {
            RuleFor(x => x.BookingId)
              .NotEmpty();

            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
