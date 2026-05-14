using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos
{
    public record DashboardStatsDto(
     int TotalUsers,
     int TotalDrivers,
     int TotalBuses,
     int TotalRoutes,
     int TotalTrips,
     int TotalBookings,
     decimal TotalRevenue,
     int TodayBookings,
     int ActiveTrips,
     int PendingBookings
 );

}
