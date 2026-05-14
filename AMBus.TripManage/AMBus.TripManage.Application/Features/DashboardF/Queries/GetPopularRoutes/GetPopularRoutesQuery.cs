using AMBus.TripManage.Application.Dtos.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.DashboardF.Queries.GetPopularRoutes
{
    public record GetPopularRoutesQuery(int Top = 5) : IRequest<IEnumerable<PopularRouteDto>>;
}
