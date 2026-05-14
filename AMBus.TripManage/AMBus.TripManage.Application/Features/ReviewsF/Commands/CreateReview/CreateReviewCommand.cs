using AMBus.TripManage.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ReviewsF.Commands.CreateReview
{
    public record CreateReviewCommand(
          Guid UserId,
          Guid TripId,
          int Rating,
          string? Comment
      ) : IRequest<ReviewDto>;
}
