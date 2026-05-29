using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos
{
    public class DashboardStatsDto
    {
        public int TotalUsers { get; set; }
        public int TotalDrivers { get; set; }
        public int TotalBuses { get; set; }
        public int TotalRoutes { get; set; }
        public int TotalTrips { get; set; }
        public int TotalBookings { get; set; } = 0;
        public decimal TotalRevenue { get; set; } = 0;
        public int TodayBookings { get; set; } = 0;
        public int ActiveTrips { get; set; } = 0;
        public int PendingBookings { get; set; } = 0;
    }
}
