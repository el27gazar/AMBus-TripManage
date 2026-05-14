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
        public RouteRepository(AppDbContext ctx) : base(ctx) { }

        public async Task<Route?> GetRouteWithStopsAsync(Guid routeId)
            => await _ctx.Routes
                .Include(r => r.Stops.OrderBy(s => s.StopOrder))
                .FirstOrDefaultAsync(r => r.Id == routeId);
    }
}
