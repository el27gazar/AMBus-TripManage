using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ReviewsF.Commands.DeleteReview
{
    public record DeleteReviewCommand(
            Guid ReviewId,
            Guid UserId,
            bool IsAdmin = false
        ) : IRequest<Unit>;
}
