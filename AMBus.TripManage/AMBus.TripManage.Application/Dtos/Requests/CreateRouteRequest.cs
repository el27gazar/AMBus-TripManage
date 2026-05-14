using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Requests
{
    public record CreateRouteRequest(
         string FromCity,
         string ToCity,
         int EstimatedDurationMinutes,
         double DistanceKm);

    public record UpdateRouteRequest(
        string FromCity,
        string ToCity,
        int EstimatedDurationMinutes,
        double DistanceKm,
        bool IsActive);

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
