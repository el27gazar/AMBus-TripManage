using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.TripDto;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Application.Features.Tripsf.Queries.GetById;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Queries.GetById
{
    public class GetTripByIdQueryHandler : IRequestHandler<GetTripByIdQuery, TripDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetTripByIdQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<TripDto> Handle(
            GetTripByIdQuery request,
            CancellationToken cancellationToken)
        {
            var trip = await _uow.Trips.GetTripWithDetailsAsync(request.TripId)
                ?? throw new NotFoundException(nameof(Trip), request.TripId);

            return _mapper.Map<TripDto>(trip);
        }
    }
}
