using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.SeatDto
{
    public record SeatDto(
         Guid Id,
         string SeatNumber,
         bool IsAvailable,
         Guid BusId
     );
}
