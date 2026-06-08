using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos.SeatDto;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Queries.GetTripSeats
{
    public class GetTripSeatsQueryHandler
           : IRequestHandler<GetTripSeatsQuery, IEnumerable<SeatAvailabilityDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetTripSeatsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SeatAvailabilityDto>> Handle(
            GetTripSeatsQuery request,
            CancellationToken cancellationToken)
        {
            var trip = await _uow.Trips.GetTripWithDetailsAsync(request.TripId)
                ?? throw new NotFoundException(nameof(Trip), request.TripId);

            var bookedSeatIds = await _uow.Bookings
                .GetBookedSeatIdsForTripAsync(request.TripId);

            return trip.Bus.Seats.OrderBy(x=>x.SeatNumber).Select(s => new SeatAvailabilityDto {
                SeatId = s.Id,
                SeatNumber = s.SeatNumber,
                IsAvailable = !bookedSeatIds.Contains(s.Id)
            });
        }
    }
}
