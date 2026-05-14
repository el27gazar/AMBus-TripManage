using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos.TripDto;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.DashboardF.Queries.GetUpcomingTrips
{
    public class GetUpcomingTripsQueryHandler
           : IRequestHandler<GetUpcomingTripsQuery, IEnumerable<TripDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetUpcomingTripsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TripDto>> Handle(
            GetUpcomingTripsQuery request,
            CancellationToken cancellationToken)
        {
            var until = DateTime.UtcNow.AddHours(request.Hours);
            var trips = await _uow.Trips
                .GetUpcomingTripsAsync(DateTime.UtcNow, until);

            return _mapper.Map<IEnumerable<TripDto>>(trips);
        }
    }
}
