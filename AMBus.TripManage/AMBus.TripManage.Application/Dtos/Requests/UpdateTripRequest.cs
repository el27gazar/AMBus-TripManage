using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Requests
{
    public record UpdateTripRequest(
          Guid DriverId,
          DateTime DepartureTime,
          DateTime ArrivalTime,
          decimal BasePrice);

    public record UpdateTripStatusRequest(string Status);
}
