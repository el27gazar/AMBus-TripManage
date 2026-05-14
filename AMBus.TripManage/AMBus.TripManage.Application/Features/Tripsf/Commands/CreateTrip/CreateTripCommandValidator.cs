using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Commands.CreateTrip
{
    public class CreateTripCommandValidator:AbstractValidator<CreateTripCommand>
    {
        public CreateTripCommandValidator()
        {
            RuleFor(x => x.RouteId)
                .NotEmpty().WithMessage("الخط مطلوب.");

            RuleFor(x => x.BusId)
                .NotEmpty().WithMessage("الباص مطلوب.");

            RuleFor(x => x.DriverId)
                .NotEmpty().WithMessage("السائق مطلوب.");

            RuleFor(x => x.DepartureTime)
                .GreaterThan(DateTime.UtcNow)
                    .WithMessage("وقت الانطلاق يجب أن يكون في المستقبل.");

            RuleFor(x => x.ArrivalTime)
                .GreaterThan(x => x.DepartureTime)
                    .WithMessage("وقت الوصول يجب أن يكون بعد وقت الانطلاق.");

            RuleFor(x => x.BasePrice)
                .GreaterThan(0)
                    .WithMessage("السعر يجب أن يكون أكبر من 0.");
        }
    }
}
