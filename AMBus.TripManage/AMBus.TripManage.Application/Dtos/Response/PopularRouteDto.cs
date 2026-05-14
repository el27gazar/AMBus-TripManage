using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Response
{
    public record PopularRouteDto(
           Guid RouteId,
           string FromCity,
           string ToCity,
           int BookingsCount,
           double AverageRating
       );
}
