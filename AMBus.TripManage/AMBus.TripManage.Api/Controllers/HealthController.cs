using AMBus.TripManage.Persistance.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMBus.TripManage.Api.Controllers
{
    [AllowAnonymous]
    public class HealthController : BaseController
    {
        private readonly AppDbContext _ctx;

        public HealthController(AppDbContext ctx) => _ctx = ctx;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> Check()
        {
            try
            {
                await _ctx.Database.CanConnectAsync();

                return Ok(new
                {
                    status = "Healthy",
                    timestamp = DateTime.UtcNow,
                    database = "Connected",
                    version = "v1.0"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(503, new
                {
                    status = "Unhealthy",
                    database = "Disconnected",
                    error = ex.Message
                });
            }
        }
    }
}
