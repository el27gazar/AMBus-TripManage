using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Response
{
    public class PopularRouteDto
    {
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public string FromName { get; set; } = string.Empty;
        public string ToName { get; set; } = string.Empty;
        public int BookingsCount { get; set; }
        public double AverageRating { get; set; }
    }
}
