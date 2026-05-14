using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ReviewsF.Commands.UpdateReview
{
    public class UpdateReviewCommandHandler
           : IRequestHandler<UpdateReviewCommand, ReviewDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ReviewDto> Handle(
            UpdateReviewCommand request,
            CancellationToken cancellationToken)
        {
            var review = await _uow.Reviews.GetReviewWithDetailsAsync(request.ReviewId)
                ?? throw new NotFoundException(nameof(Review), request.ReviewId);

            if (review.UserId != request.UserId)
                throw new UnauthorizedException("ليس لديك صلاحية تعديل هذا التقييم.");

            review.Rating = request.Rating;
            review.Comment = request.Comment;
            //review.LastModifiedDate = DateTime.UtcNow;

            _uow.Reviews.Update(review);
            await _uow.SaveChangesAsync();

            return _mapper.Map<ReviewDto>(review);
        }
    }
}
