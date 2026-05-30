using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Response;
using AMBus.TripManage.Domain.Entites;
using AMBus.TripManage.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Repositories
{
        public class DashboardRepository : IDashboardRepository
        {
            private readonly AppDbContext _ctx;
            public DashboardRepository(AppDbContext ctx) => _ctx = ctx;

            public async Task<DashboardStatsDto> GetStatsAsync()
            {
                var totalUsers = await _ctx.Users.CountAsync();
                var totalDrivers = await _ctx.Drivers.CountAsync();
                var totalBuses = await _ctx.Buses.CountAsync();
                var totalRoutes = await _ctx.Routes.CountAsync();
                var totalTrips = await _ctx.Trips.CountAsync();
                var totalBookings = await _ctx.Bookings.CountAsync();
                var todayBookings = await _ctx.Bookings.CountAsync(b =>
                    b.BookedAt.Date == DateTime.UtcNow.Date);
                var activeTrips = await _ctx.Trips.CountAsync(t =>
                    t.Status == TripStatus.InProgress);
                var pendingBookings = await _ctx.Bookings.CountAsync(b =>
                    b.Status == BookingStatus.Pending);
                var totalRevenue = await _ctx.Payments
                    .Where(p => p.Status == PaymentStatus.Paid)
                    .SumAsync(p => (decimal?)p.Amount) ?? 0;

            return new DashboardStatsDto
            {
                TotalUsers = totalUsers,
                TotalDrivers = totalDrivers,
                TotalBuses = totalBuses,
                TotalRoutes = totalRoutes,
                TotalTrips = totalTrips,
                TotalBookings = totalBookings,
                TotalRevenue = totalRevenue,
                TodayBookings = todayBookings,
                ActiveTrips = activeTrips,
                PendingBookings = pendingBookings
            };
            }

        public async Task<IEnumerable<PopularRouteDto>> GetPopularRoutesAsync(int top)
        {
            return await _ctx.Trips
                .Include(t => t.From)
                .Include(t => t.To)
                .Include(t => t.Bookings)
                .Include(t => t.Reviews)
                .GroupBy(t => new
                {
                    t.FromId,
                    t.ToId,
                    FromName = t.From.Name,
                    ToName = t.To.Name
                })
                .Select(g => new PopularRouteDto {
                        FromId = g.Key.FromId,
                        ToId = g.Key.ToId,
                        FromName = g.Key.FromName,
                        ToName = g.Key.ToName,
                        BookingsCount = g.SelectMany(t => t.Bookings)
                         .Count(b => b.Status != BookingStatus.Cancelled),
                        AverageRating = g.SelectMany(t => t.Reviews).Any()
                            ? g.SelectMany(t => t.Reviews).Average(r => r.Rating)
                            : 0

                })
                    .OrderByDescending(r => r.BookingsCount)
                    .Take(top)
                    .ToListAsync();
            }
        }
    }
