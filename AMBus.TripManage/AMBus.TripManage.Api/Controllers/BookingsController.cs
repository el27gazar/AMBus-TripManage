using AMBus.TripManage.Application.Dtos.Requests;
using AMBus.TripManage.Application.Features.BookingsF.Commands.CancelBooking;
using AMBus.TripManage.Application.Features.BookingsF.Commands.ConfirmBookingCommands;
using AMBus.TripManage.Application.Features.BookingsF.Commands.CreateBookingCommands;
using AMBus.TripManage.Application.Features.BookingsF.Queries.GetAllBookings;
using AMBus.TripManage.Application.Features.BookingsF.Queries.GetBookingById;
using AMBus.TripManage.Application.Features.BookingsF.Queries.GetMyBookings;
using AMBus.TripManage.Application.Features.BookingsF.Queries.GetTicket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMBus.TripManage.Api.Controllers
{
    [Authorize]
    public class BookingsController : BaseController
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? status,
            [FromQuery] Guid? userId,
            [FromQuery] Guid? tripId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await Mediator.Send(
                new GetAllBookingsQuery(status, userId, tripId, page, pageSize));
            return Ok(result);
        }

        [HttpGet("my")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMy(
            [FromQuery] string? status,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await Mediator.Send(
                new GetMyBookingsQuery(CurrentUserId, status, page, pageSize));
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(
                new GetBookingByIdQuery(id, CurrentUserId, IsAdmin));
            return Ok(result);
        }

        [HttpGet("{id:guid}/ticket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetTicket(Guid id)
        {
            var result = await Mediator.Send(
                new GetTicketQuery(id, CurrentUserId, IsAdmin));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(
            [FromBody] CreateBookingRequest request)
        {
            var result = await Mediator.Send(new CreateBookingCommand {
                UserId = CurrentUserId,
                TripId = request.TripId,
                Seats = request.Seats});

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await Mediator.Send(
                new CancelBookingCommand { BookingId = id, UserId = CurrentUserId, IsAdmin = IsAdmin });
            return Ok(new { message = "تم إلغاء الحجز بنجاح." });
        }

        [HttpPut("{id:guid}/confirm")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var result = await Mediator.Send(new ConfirmBookingCommand(id));
            return Ok(result);
        }
    }
}
