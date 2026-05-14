using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Queries.GetById
{
    public class GetTripByIdQueryValidator : AbstractValidator<GetTripByIdQuery>
    {
        public GetTripByIdQueryValidator()
        {
            RuleFor(x => x.TripId).NotEmpty().WithMessage("الرحلة مطلوبة.");
        }
    }
}
