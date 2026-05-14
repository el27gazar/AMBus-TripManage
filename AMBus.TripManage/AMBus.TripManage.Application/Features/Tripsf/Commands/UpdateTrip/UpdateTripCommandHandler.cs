using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos;
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

namespace AMBus.TripManage.Application.Features.Tripsf.Commands.UpdateTrip
{
    public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, TripDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateTripCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<TripDto> Handle(
            UpdateTripCommand request,
            CancellationToken cancellationToken)
        {
            var trip = await _uow.Trips.GetTripWithDetailsAsync(request.TripId)
                ?? throw new NotFoundException(nameof(Trip), request.TripId);

            if (trip.Status != TripStatus.Scheduled)
                throw new BusinessRuleException("لا يمكن تعديل رحلة غير معلقة.");

            trip.DriverId = request.DriverId;
            trip.DepartureTime = request.DepartureTime;
            trip.ArrivalTime = request.ArrivalTime;
            trip.BasePrice = request.BasePrice;
            trip.LastModifiedDate = DateTime.UtcNow;

            _uow.Trips.Update(trip);
            await _uow.SaveChangesAsync();

            var updated = await _uow.Trips.GetTripWithDetailsAsync(trip.Id) ?? trip;
            return _mapper.Map<TripDto>(updated);
        }
    }
}
