using AMBus.TripManage.Application.Dtos.Requests;
using AMBus.TripManage.Application.Features.ReviewsF.Commands.CreateReview;
using AMBus.TripManage.Application.Features.ReviewsF.Commands.DeleteReview;
using AMBus.TripManage.Application.Features.ReviewsF.Commands.UpdateReview;
using AMBus.TripManage.Application.Features.ReviewsF.Queries.GetMyReviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMBus.TripManage.Api.Controllers
{
    public class ReviewsController : BaseController
    {
        [HttpGet("my")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMy()
        {
            var result = await Mediator.Send(
                new GetMyReviewsQuery(CurrentUserId));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(
            [FromBody] CreateReviewRequest request)
        {
            var result = await Mediator.Send(new CreateReviewCommand(
                CurrentUserId,
                request.TripId,
                request.Rating,
                request.Comment));

            return Created(string.Empty, result);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateReviewRequest request)
        {
            var result = await Mediator.Send(new UpdateReviewCommand(
                id,
                CurrentUserId,
                request.Rating,
                request.Comment));

            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(
                new DeleteReviewCommand(id, CurrentUserId, IsAdmin));
            return NoContent();
        }
    }
}
