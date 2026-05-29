using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.TripDto
{
    public class TripDto
    {
        public Guid Id { get; set; }
        public string FromCity { get; set; } = string.Empty;
        public string ToCity { get; set; } = string.Empty;
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal BasePrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public int AvailableSeats { get; set; }
        public string BusType { get; set; } = string.Empty;
        public string BusPlate { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
        public Guid BusId { get; set; }
        public Guid DriverId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
