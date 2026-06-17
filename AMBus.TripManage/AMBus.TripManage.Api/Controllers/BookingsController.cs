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
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBookings(
        [FromQuery] string? status,
       [FromQuery] Guid? tripId,
       [FromQuery] int page = 1,
       [FromQuery] int pageSize = 20)
        {
            var result = await Mediator.Send(
                new GetAllBookingsQuery(null, null, tripId, page, pageSize));
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
        {
            var result = await Mediator.Send(new InitiateBookingPaymentCommand(
                CurrentUserId, request.TripId, request.Seats,
                request.PaymentMethod, request.PhoneNumber));

            return Ok(result);  // مش CreatedAtAction لأن مفيش booking لسه (لو كارت)
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

        //[HttpPut("{id:guid}/confirm")]
        //[Authorize(Roles = "Admin")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        //public async Task<IActionResult> Confirm(Guid id)
        //{
        //    var result = await Mediator.Send(new ConfirmBookingFromStripeCommand(id));
        //    return Ok(result);
        //}

    }
}
