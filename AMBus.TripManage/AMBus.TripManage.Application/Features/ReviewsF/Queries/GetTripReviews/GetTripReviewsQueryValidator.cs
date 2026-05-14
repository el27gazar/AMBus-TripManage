using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ReviewsF.Queries.GetTripReviews
{
    public class GetTripReviewsQueryValidator : AbstractValidator<GetTripReviewsQuery>
    {
        public GetTripReviewsQueryValidator()
        {
            RuleFor(x => x.TripId).NotEmpty().WithMessage("الرحلة مطلوبة.");
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("التقييم بين 1 و 5.")
                .When(x => x.Rating.HasValue);
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 50);
        }
    }
}
