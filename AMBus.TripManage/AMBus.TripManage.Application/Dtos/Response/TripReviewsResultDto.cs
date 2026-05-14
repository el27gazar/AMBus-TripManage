using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Response
{
    public record TripReviewsResultDto(
            List<ReviewDto> Reviews,
            double AverageRating,
            int TotalReviews,
            int Page,
            int PageSize,
            int TotalPages
        );
}
