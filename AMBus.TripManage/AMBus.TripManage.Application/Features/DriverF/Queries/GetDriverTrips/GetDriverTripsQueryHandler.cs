using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos.TripDto;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.DriverF.Queries.GetDriverTrips
{
    public class GetDriverTripsQueryHandler
         : IRequestHandler<GetDriverTripsQuery, IEnumerable<TripDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetDriverTripsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TripDto>> Handle(
            GetDriverTripsQuery request,
            CancellationToken cancellationToken)
        {
            // تحقق إن السائق موجود
            var driver = await _uow.Drivers.GetByIdAsync(request.DriverId)
                ?? throw new NotFoundException(nameof(Driver), request.DriverId);

            var trips = await _uow.Trips
                .GetTripsByDriverAsync(request.DriverId, request.Status);

            return _mapper.Map<IEnumerable<TripDto>>(trips);
        }
    }
}