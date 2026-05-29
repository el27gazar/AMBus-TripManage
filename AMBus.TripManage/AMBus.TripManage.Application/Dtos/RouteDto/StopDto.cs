using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.RouteDto
{
    public class StopDto
    {
        public Guid Id { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string? StationAddress { get; set; }
        public int StopOrder { get; set; }
        public int ArrivalOffsetMinutes { get; set; }
        public Guid RouteId { get; set; }
    }
}
