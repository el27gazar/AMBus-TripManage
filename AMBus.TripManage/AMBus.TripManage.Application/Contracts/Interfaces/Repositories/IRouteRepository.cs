using AMBus.TripManage.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces.Repositories
{
    public interface IRouteRepository : IGenericRepository<Route>
    {
        Task<Route?> GetRouteWithStopsAsync(Guid routeId);
        Task<IEnumerable<Route>> GetAllActiveRoutesAsync();
        Task<bool> ExistsWithNameAsync(string name);
        Task<bool> HasTripsAsync(Guid routeId);
    }
}
