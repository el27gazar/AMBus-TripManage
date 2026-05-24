using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.RouteDto
{
    public record RouteDto(
          Guid Id,
          string Name,
          bool IsActive,
          List<StopDto> Stops,
          DateTime CreatedAt
      );
}
