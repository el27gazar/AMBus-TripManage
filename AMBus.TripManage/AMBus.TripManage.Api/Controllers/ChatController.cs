using AMBus.TripManage.Application.Dtos.Chat;
using AMBus.TripManage.Application.Features.ChatF.Commands.AssignAdmin;
using AMBus.TripManage.Application.Features.ChatF.Commands.CloseConversation;
using AMBus.TripManage.Application.Features.ChatF.Commands.CreateConversation;
using AMBus.TripManage.Application.Features.ChatF.Commands.SendMessage;
using AMBus.TripManage.Application.Features.ChatF.Queries.GetAllConversations;
using AMBus.TripManage.Application.Features.ChatF.Queries.GetMessages;
using AMBus.TripManage.Application.Features.ChatF.Queries.GetMyConversations;
using AMBus.TripManage.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMBus.TripManage.Api.Controllers
{
    [Authorize]
    public class ChatController : BaseController
    {

        [HttpGet("my")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMy()
        {
            var result = await Mediator.Send(
                new GetMyConversationsQuery(CurrentUserId));
            return Ok(result);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? status,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await Mediator.Send(
                new GetAllConversationsQuery(status, page, pageSize));
            return Ok(result);
        }

  
        [HttpGet("{id:guid}/messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessages(
            Guid id,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50)
        {
            var result = await Mediator.Send(
                new GetMessagesQuery(
                    id, CurrentUserId, IsAdmin, page, pageSize));
            return Ok(result);
        }


        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] CreateConversationDto dto)
        {
            var result = await Mediator.Send(
                new CreateConversationCommand(
                    CurrentUserId,
                    dto.Subject,
                    dto.FirstMessage));

            return Created(string.Empty, result);
        }


        [HttpPost("{id:guid}/messages")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> SendMessage(
            Guid id,
            [FromBody] SendMessageRequest request)
        {
            var result = await Mediator.Send(
                new SendMessageCommand {
                    ConversationId = id.ToString(),
                    Content = request.Content,
                    CurrentUserId = CurrentUserId,
                    IsAdmin=IsAdmin,
                    CurrentUserName= CurrentUserName
                });

            return Created(string.Empty, result);
        }


        [HttpPut("{id:guid}/assign")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Assign(Guid id)
        {
            await Mediator.Send(
                new AssignAdminCommand(id, CurrentUserId));
            return Ok(new { message = "تم تعيينك على المحادثة." });
        }

 
        [HttpPut("{id:guid}/close")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Close(Guid id)
        {
            await Mediator.Send(
                new CloseConversationCommand(id, CurrentUserId));
            return Ok(new { message = "تم إغلاق المحادثة." });
        }
    }

    public record SendMessageRequest(string Content);
}
