using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Queries.GetAllTrips
{
    public class GetAllTripsQueryValidator : AbstractValidator<GetAllTripsQuery>
    {
        public GetAllTripsQueryValidator()
        {
           

            RuleFor(x => x.Date)
                .GreaterThanOrEqualTo(DateTime.Today)
                    .WithMessage("التاريخ لا يمكن أن يكون في الماضي.")
                .When(x => x.Date.HasValue);

            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("رقم الصفحة يجب أن يكون أكبر من 0.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 50).WithMessage("حجم الصفحة بين 1 و 50.");
        }
    }
}
