using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Queries.GetAllBookings
{
    public class GetAllBookingsQueryValidator : AbstractValidator<GetAllBookingsQuery>
    {
        public GetAllBookingsQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("رقم الصفحة يجب أن يكون أكبر من 0.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("حجم الصفحة بين 1 و 100.");
        }
    }
}
