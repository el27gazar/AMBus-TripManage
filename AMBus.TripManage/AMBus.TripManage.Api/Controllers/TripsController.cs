using AMBus.TripManage.Application.Dtos.Requests;
using AMBus.TripManage.Application.Features.ReviewsF.Queries.GetTripReviews;
using AMBus.TripManage.Application.Features.Tripsf.Commands.CreateTrip;
using AMBus.TripManage.Application.Features.Tripsf.Commands.DeleteTrip;
using AMBus.TripManage.Application.Features.Tripsf.Commands.UpdateTrip;
using AMBus.TripManage.Application.Features.Tripsf.Commands.UpdateTripStatus;
using AMBus.TripManage.Application.Features.Tripsf.Queries.GetAllTrips;
using AMBus.TripManage.Application.Features.Tripsf.Queries.GetById;
using AMBus.TripManage.Application.Features.Tripsf.Queries.GetTripSeats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMBus.TripManage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? fromCity,
            [FromQuery] string? toCity,
            [FromQuery] DateTime? date,
            [FromQuery] int seats = 1,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await Mediator.Send(
                new GetAllTripsQuery(fromCity, toCity, date, seats, page, pageSize));
            return Ok(result);
        }

        [HttpGet("GetAllTrips")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTrip()
        {
            var result = await Mediator.Send(
                new GetAllTripsQuery(null, null, null, 0, 1, 50));
            return Ok(result);
        }

        [HttpGet("{id:guid}", Name = "GetTripById")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetTripByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("{id:guid}/seats")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSeats(Guid id)
        {
            var result = await Mediator.Send(new GetTripSeatsQuery(id));
            return Ok(result);
        }

        [HttpGet("{id:guid}/reviews")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviews(
            Guid id,
            [FromQuery] int? rating,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await Mediator.Send(
                new GetTripReviewsQuery(id, rating, page, pageSize));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(
            [FromBody] CreateTripCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateTripRequest request)
        {
            var result = await Mediator.Send(new UpdateTripCommand(
                id,
                request.DriverId,
                request.DepartureTime,
                request.ArrivalTime,
                request.BasePrice));
            return Ok(result);
        }

        [HttpPut("{id:guid}/status")]
        [Authorize(Roles = "Admin,Driver")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> UpdateStatus(
            Guid id,
            [FromBody] UpdateTripStatusRequest request)
        {
            var result = await Mediator.Send(
                new UpdateTripStatusCommand(id, request.Status));
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteTripCommand(id));
            return NoContent();
        }
    }
}
