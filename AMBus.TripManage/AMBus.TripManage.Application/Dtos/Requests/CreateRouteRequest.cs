using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Requests
{
    // CreateRouteRequest.cs
    public class CreateRouteRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }

    // UpdateRouteRequest.cs
    public class UpdateRouteRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
    public class CreateStopRequest
    {
        public string CityName { get; set; } = string.Empty;
        public string? StationAddress { get; set; }
        public int StopOrder { get; set; }
        public int ArrivalOffsetMinutes { get; set; }
    }

    public class UpdateStopRequest
    {
        public string CityName { get; set; } = string.Empty;
        public string? StationAddress { get; set; }
        public int StopOrder { get; set; }
        public int ArrivalOffsetMinutes { get; set; }
    }
}
