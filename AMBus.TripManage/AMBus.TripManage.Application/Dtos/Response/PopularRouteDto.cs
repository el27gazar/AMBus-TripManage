using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Response
{
    public record PopularRouteDto(
      Guid FromId,
      Guid ToId,
      string FromName,
      string ToName,
      int BookingsCount,
      double AverageRating
  );
}
