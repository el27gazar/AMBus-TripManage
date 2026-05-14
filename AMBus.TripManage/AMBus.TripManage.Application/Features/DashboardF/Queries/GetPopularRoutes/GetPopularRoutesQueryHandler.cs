using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.DashboardF.Queries.GetPopularRoutes
{
    public class GetPopularRoutesQueryHandler
          : IRequestHandler<GetPopularRoutesQuery, IEnumerable<PopularRouteDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetPopularRoutesQueryHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<PopularRouteDto>> Handle(
            GetPopularRoutesQuery request,
            CancellationToken cancellationToken)
        {
            return await _uow.Dashboard.GetPopularRoutesAsync(request.Top);
        }
    }
}
