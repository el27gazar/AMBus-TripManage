using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Requests
{
    public class CreateReviewRequest
    {
        public Guid TripId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }

    public class UpdateReviewRequest
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}