using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.DashboardF.Queries.GetPopularRoutes
{
    public class GetPopularRoutesQueryValidator
            : AbstractValidator<GetPopularRoutesQuery>
    {
        public GetPopularRoutesQueryValidator()
        {
            RuleFor(x => x.Top)
                .InclusiveBetween(1, 20)
                    .WithMessage("Top يجب أن يكون بين 1 و 20.");
        }
    }

}
