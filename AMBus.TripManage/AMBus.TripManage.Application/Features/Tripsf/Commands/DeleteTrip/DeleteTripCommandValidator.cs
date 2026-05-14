using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Commands.DeleteTrip
{
    public class DeleteTripCommandValidator : AbstractValidator<DeleteTripCommand>
    {
        public DeleteTripCommandValidator()
        {
            RuleFor(x => x.TripId).NotEmpty().WithMessage("الرحلة مطلوبة.");
        }
    }
}
