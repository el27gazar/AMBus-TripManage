using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.RouteDto
{
    public record StopDto(
         Guid Id,
         string CityName,
         string? StationAddress,
         int StopOrder,
         int ArrivalOffsetMinutes,
         Guid RouteId
     );
}
