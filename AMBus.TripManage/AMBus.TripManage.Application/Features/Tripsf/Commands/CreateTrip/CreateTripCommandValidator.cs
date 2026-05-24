using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Commands.CreateTrip
{
    public class CreateTripCommandValidator : AbstractValidator<CreateTripCommand>
    {
        public CreateTripCommandValidator()
        {
            RuleFor(x => x.FromId)
                .NotEmpty().WithMessage("Departure city is required.");

            RuleFor(x => x.ToId)
                .NotEmpty().WithMessage("Arrival city is required.")
                .NotEqual(x => x.FromId).WithMessage("Arrival city must be different from departure city.");

            RuleFor(x => x.BusId)
                .NotEmpty().WithMessage("Bus is required.");

            RuleFor(x => x.DriverId)
                .NotEmpty().WithMessage("Driver is required.");

            RuleFor(x => x.DepartureTime)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Departure time must be in the future.");

            RuleFor(x => x.ArrivalTime)
                .GreaterThan(x => x.DepartureTime)
                .WithMessage("Arrival time must be after departure time.");

            RuleFor(x => x.BasePrice)
                .GreaterThan(0)
                .WithMessage("Base price must be greater than 0.");
        }
    }
}
