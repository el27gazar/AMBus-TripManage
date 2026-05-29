using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Response
{
    public class TripReviewsResultDto
    {
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
        
}
