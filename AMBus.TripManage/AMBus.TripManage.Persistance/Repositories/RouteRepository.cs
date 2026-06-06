using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
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
    public class RouteRepository : GenericRepository<Route>, IRouteRepository
    {
        private readonly AppDbContext _ctx;

        public RouteRepository(AppDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<Route?> GetRouteWithStopsAsync(Guid routeId)
        {
            return await _ctx.Routes
                .Include(r => r.Stops.OrderBy(s => s.StopOrder))
                .FirstOrDefaultAsync(r => r.Id == routeId);
        }
        public async Task<bool> HasTripsAsync(Guid routeId)
        {
           return await _ctx.Trips.AnyAsync(t =>
              t.FromId == routeId || t.ToId == routeId);
        }
        public async Task<IEnumerable<Route>> GetAllActiveRoutesAsync()
        {
            return await _ctx.Routes
                .Where(r => r.IsActive)
                .OrderBy(r => r.Name)
                .ToListAsync();
        }
        public async Task<bool> ExistsWithNameAsync(string name)
        {
           return await _ctx.Routes.AnyAsync(r =>
               r.Name.ToLower() == name.ToLower());
        }
    }
}
