using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.RouteDto
{
    public record RouteDto(
         Guid Id,
         string FromCity,
         string ToCity,
         int EstimatedDurationMinutes,
         double DistanceKm,
         bool IsActive,
         List<StopDto> Stops,
         DateTime CreatedAt
     );
}
