using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.TripDto
{
    public record TripDto(
     Guid Id,
     string FromCity,
     string ToCity,
     DateTime DepartureTime,
     DateTime ArrivalTime,
     decimal BasePrice,
     string Status,
     int AvailableSeats,
     string BusType,
     string BusPlate,
     string DriverName,
     Guid RouteId,
     Guid BusId,
     Guid DriverId,
     DateTime CreatedAt
 );
}
