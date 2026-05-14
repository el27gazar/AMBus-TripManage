using AMBus.TripManage.Application.Features.NotificationF.Commands.DeleteNotification;
using AMBus.TripManage.Application.Features.NotificationF.Commands.MarkAllAsRead;
using AMBus.TripManage.Application.Features.NotificationF.Commands.MarkAsRead;
using AMBus.TripManage.Application.Features.NotificationF.Commands.SendNotification;
using AMBus.TripManage.Application.Features.NotificationF.Queries.GetMyNotifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMBus.TripManage.Api.Controllers
{
    [Authorize]
    public class NotificationsController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMy([FromQuery] bool? isRead)
        {
            var result = await Mediator.Send(
                new GetMyNotificationsQuery(CurrentUserId, isRead));
            return Ok(result);
        }

        [HttpPut("{id:guid}/read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkAsRead(Guid id)
        {
            await Mediator.Send(
                new MarkNotificationReadCommand(id, CurrentUserId));
            return Ok(new { message = "تم تعليم الإشعار كمقروء." });
        }

        [HttpPut("read-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> MarkAllAsRead()
        {
            await Mediator.Send(
                new MarkAllNotificationsReadCommand(CurrentUserId));
            return Ok(new { message = "تم تعليم كل الإشعارات كمقروءة." });
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(
                new DeleteNotificationCommand(id, CurrentUserId));
            return NoContent();
        }

        [HttpPost("send")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Send(
            [FromBody] SendNotificationCommand command)
        {
            await Mediator.Send(command);
            return Ok(new { message = "تم إرسال الإشعارات بنجاح." });
        }
    }
}
