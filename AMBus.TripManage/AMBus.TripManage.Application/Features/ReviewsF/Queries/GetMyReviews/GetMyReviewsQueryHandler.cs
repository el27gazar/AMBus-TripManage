using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ReviewsF.Queries.GetMyReviews
{
    public class GetMyReviewsQueryHandler
           : IRequestHandler<GetMyReviewsQuery, IEnumerable<ReviewDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetMyReviewsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewDto>> Handle(
            GetMyReviewsQuery request,
            CancellationToken cancellationToken)
        {
            var reviews = await _uow.Reviews.GetUserReviewsAsync(request.UserId);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }
    }
}
