using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.DashboardF.Queries.GetDashboardStats
{
    public class GetDashboardStatsQueryHandler
           : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
    {
        private readonly IUnitOfWork _uow;

        public GetDashboardStatsQueryHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<DashboardStatsDto> Handle(
            GetDashboardStatsQuery request,
            CancellationToken cancellationToken)
        {
            var stats = await _uow.Dashboard.GetStatsAsync();
            return stats;
        }
    }
}
