using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces.Repositories
{
    public interface IDashboardRepository
    {
        Task<DashboardStatsDto> GetStatsAsync();
        Task<IEnumerable<PopularRouteDto>> GetPopularRoutesAsync(int top);
    }

}
