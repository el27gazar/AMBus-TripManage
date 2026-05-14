using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Response;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ReviewsF.Queries.GetTripReviews
{
    public class GetTripReviewsQueryHandler
            : IRequestHandler<GetTripReviewsQuery, TripReviewsResultDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetTripReviewsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<TripReviewsResultDto> Handle(
            GetTripReviewsQuery request,
            CancellationToken cancellationToken)
        {
            var (reviews, total, avg) = await _uow.Reviews
                .GetTripReviewsPagedAsync(
                    request.TripId,
                    request.Rating,
                    request.Page,
                    request.PageSize);

            return new TripReviewsResultDto(
                Reviews: _mapper.Map<List<ReviewDto>>(reviews),
                AverageRating: Math.Round(avg, 1),
                TotalReviews: total,
                Page: request.Page,
                PageSize: request.PageSize,
                TotalPages: (int)Math.Ceiling(total / (double)request.PageSize)
            );
        }
    }
}
