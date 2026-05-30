using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.TripDto;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Queries.GetAllTrips
{
    public class GetAllTripsQueryHandler
        : IRequestHandler<GetAllTripsQuery, PagedResultDto<TripDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllTripsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<TripDto>> Handle(
            GetAllTripsQuery request,
            CancellationToken cancellationToken)
        {
            var (trips, total) = await _uow.Trips.SearchTripsPagedAsync(
                request.FromCity,
                request.ToCity,
                request.Date,
                request.Seats,
                request.Page,
                request.PageSize);

            return new PagedResultDto<TripDto> {
                Items = _mapper.Map<List<TripDto>>(trips),
                TotalCount = total,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(total / (double)request.PageSize)
            };
        }
    }
}
