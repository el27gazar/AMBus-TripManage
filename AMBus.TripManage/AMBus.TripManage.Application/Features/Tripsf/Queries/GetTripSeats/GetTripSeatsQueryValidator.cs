using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Queries.GetTripSeats
{
    public class GetTripSeatsQueryValidator : AbstractValidator<GetTripSeatsQuery>
    {
        public GetTripSeatsQueryValidator()
        {
            RuleFor(x => x.TripId).NotEmpty().WithMessage("الرحلة مطلوبة.");
        }
    }
}
