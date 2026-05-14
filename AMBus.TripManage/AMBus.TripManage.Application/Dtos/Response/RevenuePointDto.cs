using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Response
{
    public record RevenuePointDto(
            string Period,         // "Jan 2025" | "2025-08-01"
            decimal Amount,
            int BookingsCount
        );
}
