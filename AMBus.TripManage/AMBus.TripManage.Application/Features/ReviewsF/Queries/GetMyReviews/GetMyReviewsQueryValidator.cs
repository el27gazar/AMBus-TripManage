using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ReviewsF.Queries.GetMyReviews
{
    public class GetMyReviewsQueryValidator : AbstractValidator<GetMyReviewsQuery>
    {
        public GetMyReviewsQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("المستخدم مطلوب.");
        }
    }
}
