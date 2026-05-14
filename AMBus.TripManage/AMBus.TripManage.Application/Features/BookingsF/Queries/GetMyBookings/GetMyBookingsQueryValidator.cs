using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Queries.GetMyBookings
{
    public class GetMyBookingsQueryValidator : AbstractValidator<GetMyBookingsQuery>
    {
        private static readonly string[] AllowedStatuses =
            { "Pending", "Confirmed", "Cancelled", "Completed" };

        public GetMyBookingsQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("المستخدم مطلوب.");

            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("رقم الصفحة يجب أن يكون أكبر من 0.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 50)
                    .WithMessage("حجم الصفحة بين 1 و 50.");

            RuleFor(x => x.Status)
                .Must(s => AllowedStatuses.Contains(s))
                    .WithMessage($"الحالة يجب أن تكون: {string.Join(", ", AllowedStatuses)}.")
                .When(x => !string.IsNullOrEmpty(x.Status));
        }
    }
}
