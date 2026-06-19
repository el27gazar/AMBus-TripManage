using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AMBus.TripManage.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        private ISender? _mediator;

        protected ISender Mediator =>
            _mediator ??= HttpContext.RequestServices
                                     .GetRequiredService<ISender>();

        protected Guid CurrentUserId =>
            Guid.TryParse(
                User.FindFirstValue(ClaimTypes.NameIdentifier),
                out var id) ? id : Guid.Empty;

        protected string CurrentUserRole =>
            User.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

        protected bool IsAdmin =>
            CurrentUserRole.Equals("Admin",
                StringComparison.OrdinalIgnoreCase);

        protected string CurrentUserName =>
            User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
    }

}
