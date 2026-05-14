using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ReviewsF.Commands.CreateReview
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("المستخدم مطلوب.");

            RuleFor(x => x.TripId)
                .NotEmpty().WithMessage("الرحلة مطلوبة.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5)
                    .WithMessage("التقييم يجب أن يكون بين 1 و 5.");

            RuleFor(x => x.Comment)
                .MaximumLength(500)
                    .WithMessage("التعليق لا يتجاوز 500 حرف.")
                .When(x => !string.IsNullOrEmpty(x.Comment));
        }
    }
}
