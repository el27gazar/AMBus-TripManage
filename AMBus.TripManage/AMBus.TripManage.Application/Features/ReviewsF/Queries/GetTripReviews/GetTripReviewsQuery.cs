using AMBus.TripManage.Application.Dtos.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ReviewsF.Queries.GetTripReviews
{
    public record GetTripReviewsQuery(
        Guid TripId,
        int? Rating,        // فلتر اختياري
        int Page = 1,
        int PageSize = 10
    ) : IRequest<TripReviewsResultDto>;
}
