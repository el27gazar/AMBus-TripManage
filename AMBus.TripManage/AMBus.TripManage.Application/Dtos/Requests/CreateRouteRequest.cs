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
    public record CreateStopRequest(
        string CityName,
        string? StationAddress,
        int StopOrder,
        int ArrivalOffsetMinutes);

    public record UpdateStopRequest(
        string CityName,
        string? StationAddress,
        int StopOrder,
        int ArrivalOffsetMinutes);
}
