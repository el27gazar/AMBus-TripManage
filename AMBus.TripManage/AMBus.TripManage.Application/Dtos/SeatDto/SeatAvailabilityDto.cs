using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.SeatDto
{
    public record SeatAvailabilityDto(
         Guid SeatId,
         string SeatNumber,
         bool IsAvailable
     );
}
