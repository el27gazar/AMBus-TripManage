using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.DashboardF.Queries.GetRevenueChart
{
    public class GetRevenueChartQueryHandler
        : IRequestHandler<GetRevenueChartQuery, IEnumerable<RevenuePointDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetRevenueChartQueryHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<RevenuePointDto>> Handle(
            GetRevenueChartQuery request,
            CancellationToken cancellationToken)
        {
            var payments = await _uow.Payments
                .GetPaidInRangeAsync(request.From, request.To);

            if (request.GroupBy == "day")
            {
                return payments
                    .GroupBy(p => p.PaidAt!.Value)
                    .OrderBy(g => g.Key)
                    .Select(g => new RevenuePointDto(
                        Period: g.Key.ToString("yyyy-MM-dd"),
                        Amount: g.Sum(p => p.Amount),
                        BookingsCount: g.Count()
                    ));
            }
            else
            {
                return payments
                    .GroupBy(p => new { p.PaidAt!.Value.Year, p.PaidAt!.Value.Month })
                    .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                    .Select(g => new RevenuePointDto(
                        Period: new DateTime(g.Key.Year, g.Key.Month, 1)
                                            .ToString("MMM yyyy"),
                        Amount: g.Sum(p => p.Amount),
                        BookingsCount: g.Count()
                    ));
            }
        }
    }
}
