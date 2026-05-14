using AMBus.TripManage.Application.Dtos.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.DashboardF.Queries.GetRevenueChart
{
    public record GetRevenueChartQuery(
            DateTime From,
            DateTime To,
            string GroupBy = "month"   // "day" | "month"
        ) : IRequest<IEnumerable<RevenuePointDto>>;
}
