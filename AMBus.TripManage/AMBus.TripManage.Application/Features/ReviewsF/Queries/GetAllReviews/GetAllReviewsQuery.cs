using AMBus.TripManage.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ReviewsF.Queries.GetAllReviews
{
    public record GetAllReviewsQuery(
      int? Rating,
      Guid? TripId,
      Guid? UserId,
      DateTime? From,
      DateTime? To,
      int Page = 1,
      int PageSize = 20
  ) : IRequest<PagedResultDto<AdminReviewDto>>;

    public record AdminReviewDto(
        Guid Id,
        int Rating,
        string? Comment,
        DateTime CreatedAt,
        Guid UserId,
        string UserFullName,
        Guid TripId
    );
}
