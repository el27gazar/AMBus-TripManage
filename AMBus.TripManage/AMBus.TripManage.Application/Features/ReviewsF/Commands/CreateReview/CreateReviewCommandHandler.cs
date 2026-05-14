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

namespace AMBus.TripManage.Application.Features.ReviewsF.Commands.CreateReview
{
    public class CreateReviewCommandHandler
         : IRequestHandler<CreateReviewCommand, ReviewDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ReviewDto> Handle(
            CreateReviewCommand request,
            CancellationToken cancellationToken)
        {
            // الرحلة موجودة؟
            var trip = await _uow.Trips.GetByIdAsync(request.TripId)
                ?? throw new NotFoundException(nameof(Trip), request.TripId);

            // الرحلة مكتملة؟
            if (trip.Status != TripStatus.Completed)
                throw new BusinessRuleException("يمكن تقييم الرحلات المكتملة فقط.");

            // المستخدم حجز على الرحلة دي؟
            var hasBooking = await _uow.Bookings
                .HasCompletedBookingForTripAsync(request.UserId, request.TripId);
            if (!hasBooking)
                throw new BusinessRuleException("يمكنك تقييم الرحلات التي سافرت فيها فقط.");

            // مش عامل review قبل كده؟
            var alreadyReviewed = await _uow.Reviews
                .HasUserReviewedTripAsync(request.UserId, request.TripId);
            if (alreadyReviewed)
                throw new ConflictException("لقد قمت بتقييم هذه الرحلة من قبل.");

            var review = new Review
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                TripId = request.TripId,
                Rating = request.Rating,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _uow.Reviews.AddAsync(review);
            await _uow.SaveChangesAsync();

            var created = await _uow.Reviews.GetReviewWithDetailsAsync(review.Id) ?? review;
            return _mapper.Map<ReviewDto>(created);
        }
    }
}
