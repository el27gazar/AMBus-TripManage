using AMBus.TripManage.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ReviewsF.Queries.GetMyReviews
{
    public record GetMyReviewsQuery(Guid UserId) : IRequest<IEnumerable<ReviewDto>>;
}
