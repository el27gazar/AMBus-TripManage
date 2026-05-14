using AMBus.TripManage.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ReviewsF.Commands.UpdateReview
{
    public record UpdateReviewCommand(
           Guid ReviewId,
           Guid UserId,
           int Rating,
           string? Comment
       ) : IRequest<ReviewDto>;
}
