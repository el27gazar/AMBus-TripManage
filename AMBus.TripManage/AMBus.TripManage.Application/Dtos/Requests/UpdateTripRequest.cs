using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Requests
{
    public class UpdateTripRequest
    {
        public Guid DriverId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal BasePrice { get; set; }
    }

    public class UpdateTripStatusRequest
    {
        public string Status { get; set; } = string.Empty;
    }
}
