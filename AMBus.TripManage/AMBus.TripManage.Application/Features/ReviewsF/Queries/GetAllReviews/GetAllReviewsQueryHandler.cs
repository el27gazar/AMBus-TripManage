using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ReviewsF.Queries.GetAllReviews
{
    public class GetAllReviewsQueryHandler
        : IRequestHandler<GetAllReviewsQuery, PagedResultDto<AdminReviewDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllReviewsQueryHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<PagedResultDto<AdminReviewDto>> Handle(
            GetAllReviewsQuery request, CancellationToken ct)
        {
            var (items, totalCount) = await _uow.Reviews.GetAllWithFiltersAsync(
                request.Rating, request.TripId, request.UserId,
                request.From, request.To, request.Page, request.PageSize);

            var dtoItems = items.Select(r => new AdminReviewDto(
                r.Id,
                r.Rating,
                r.Comment,
                r.CreatedAt,
                r.UserId,
                r.User.FullName,
                r.TripId
            )).ToList();

            return new PagedResultDto<AdminReviewDto>
            {
                Items = dtoItems,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = request.PageSize == 0
                    ? 0
                    : (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };
        }
    }
}
