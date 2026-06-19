using Microsoft.EntityFrameworkCore;
using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Domain.Entites;
using AMBus.TripManage.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Repositories
{
    public class TripRepository : GenericRepository<Trip>, ITripRepository
    {
        public TripRepository(AppDbContext ctx) : base(ctx) { }

        public async Task<Trip?> GetTripWithDetailsAsync(Guid tripId)
            => await _ctx.Trips
                .Include(t => t.From)                               
                .Include(t => t.To)                                 
                .Include(t => t.Bus)
                    .ThenInclude(b => b.Seats)
                .Include(t => t.Driver)
                    .ThenInclude(d => d.User)
                .Include(t => t.Reviews)
                .FirstOrDefaultAsync(t => t.Id == tripId);
     

        public async Task<bool> HasBusConflictAsync(
            Guid busId, DateTime departure, DateTime arrival)
            => await _ctx.Trips.AnyAsync(t =>
                t.BusId == busId &&
                t.Status == TripStatus.Scheduled &&
                t.DepartureTime < arrival &&
                t.ArrivalTime > departure);

        public async Task<IEnumerable<Trip>> GetUpcomingTripsAsync(
            DateTime from, DateTime until)
            => await _ctx.Trips
                .Include(t => t.From)                               
                .Include(t => t.To)                                 
                .Include(t => t.Bus)
                .Include(t => t.Driver).ThenInclude(d => d.User)
                .Where(t =>
                    t.Status == TripStatus.Scheduled &&
                    t.DepartureTime >= from &&
                    t.DepartureTime <= until)
                .OrderBy(t => t.DepartureTime)
                .ToListAsync();

        public async Task<(IEnumerable<Trip> Items, int Total)> SearchTripsPagedAsync(
            string? fromCity, string? toCity, DateTime? date,
            int seats, int page, int pageSize)
        {
            var query = _ctx.Trips
                .Include(t => t.From)                               
                .Include(t => t.To)                                
                .Include(t => t.Bus)
                .Include(t => t.Driver).ThenInclude(d => d.User)
                .Where(t =>
                    t.Status == TripStatus.Scheduled &&
                    t.AvailableSeats >= seats)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(fromCity))
                query = query.Where(t =>
                    t.From.Name.Contains(fromCity));               

            if (!string.IsNullOrWhiteSpace(toCity))
                query = query.Where(t =>
                    t.To.Name.Contains(toCity));                    

            if (date.HasValue)
                query = query.Where(t =>
                    t.DepartureTime.Date == date.Value.Date);

            var total = await query.CountAsync();
            var items = await query
                .OrderBy(t => t.DepartureTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }
        public async Task<IEnumerable<Trip>> GetExpiredTripsAsync(DateTime now)
          => await _ctx.Trips
                .Include(t => t.Driver)
                .Include(t => t.Bus)
                .Where(t =>
                     t.ArrivalTime <= now &&
                     (t.Status == TripStatus.Scheduled || t.Status == TripStatus.InProgress))
                .ToListAsync();

        //override GetByIdAsync to include Driver and Bus details
        public async Task<Trip?> GetByIdAsync(Guid id)
    => await _ctx.Trips
        .Include(t => t.Driver)
        .Include(t => t.Bus)
        .FirstOrDefaultAsync(t => t.Id == id);


        public async Task<IEnumerable<Trip>> GetTripsByDriverAsync(Guid driverId, string? status)
        {
            var query = _ctx.Trips
                .Include(t => t.From)
                .Include(t => t.To)
                .Include(t => t.Bus)
                .Include(t => t.Driver).ThenInclude(d => d.User)
                .Where(t => t.DriverId == driverId)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                if (Enum.TryParse<TripStatus>(status, ignoreCase: true, out var parsedStatus))
                    query = query.Where(t => t.Status == parsedStatus);
                else if (status.Equals("Upcoming", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(t =>
                        t.Status == TripStatus.Scheduled &&
                        t.DepartureTime > DateTime.UtcNow);
            }

            return await query
                .OrderByDescending(t => t.DepartureTime)
                .ToListAsync();
        }
    }
}
