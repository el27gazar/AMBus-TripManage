using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ReviewsF.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public DeleteReviewCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Unit> Handle(
            DeleteReviewCommand request,
            CancellationToken cancellationToken)
        {
            var review = await _uow.Reviews.GetByIdAsync(request.ReviewId)
                ?? throw new NotFoundException(nameof(Review), request.ReviewId);

            if (!request.IsAdmin && review.UserId != request.UserId)
                throw new UnauthorizedException("ليس لديك صلاحية حذف هذا التقييم.");

            _uow.Reviews.Delete(review);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
