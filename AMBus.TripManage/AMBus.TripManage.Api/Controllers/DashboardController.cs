using AMBus.TripManage.Application.Features.DashboardF.Queries.GetDashboardStats;
using AMBus.TripManage.Application.Features.DashboardF.Queries.GetPopularRoutes;
using AMBus.TripManage.Application.Features.DashboardF.Queries.GetRevenueChart;
using AMBus.TripManage.Application.Features.DashboardF.Queries.GetUpcomingTrips;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMBus.TripManage.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : BaseController
    {
        [HttpGet("stats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStats()
        {
            var result = await Mediator.Send(new GetDashboardStatsQuery());
            return Ok(result);
        }

        [HttpGet("revenue")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRevenue(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            [FromQuery] string groupBy = "month")
        {
            var result = await Mediator.Send(
                new GetRevenueChartQuery(from, to, groupBy));
            return Ok(result);
        }

        [HttpGet("popular-routes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPopularRoutes(
            [FromQuery] int top = 5)
        {
            var result = await Mediator.Send(
                new GetPopularRoutesQuery(top));
            return Ok(result);
        }

        [HttpGet("upcoming-trips")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUpcomingTrips(
            [FromQuery] int hours = 24)
        {
            var result = await Mediator.Send(
                new GetUpcomingTripsQuery(hours));
            return Ok(result);
        }

        [HttpGet("Revenue-Chart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRevenueCharts(
      [FromQuery] DateTime From, [FromQuery] DateTime To)
        {
            var result = await Mediator.Send(new GetRevenueChartQuery(From, To));
            return Ok(result);
        }
    }
}
