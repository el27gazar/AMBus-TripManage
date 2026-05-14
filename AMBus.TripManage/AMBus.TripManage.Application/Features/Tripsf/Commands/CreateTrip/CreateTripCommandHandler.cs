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

namespace AMBus.TripManage.Application.Features.Tripsf.Commands.CreateTrip
{
    public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, TripDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateTripCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<TripDto> Handle(
            CreateTripCommand request,
            CancellationToken cancellationToken)
        {
            // التحقق من وجود الـ Route
            var route = await _uow.Routes.GetByIdAsync(request.RouteId)
                ?? throw new NotFoundException(nameof(Route), request.RouteId);

            // التحقق من وجود الـ Bus
            var bus = await _uow.Buses.GetByIdAsync(request.BusId)
                ?? throw new NotFoundException(nameof(Bus), request.BusId);

            // التحقق من وجود الـ Driver
            var driver = await _uow.Drivers.GetByIdAsync(request.DriverId)
                ?? throw new NotFoundException(nameof(Driver), request.DriverId);

            // التأكد إن الـ Driver متاح
            if (!driver.IsAvailable)
                throw new BusinessRuleException("السائق غير متاح في هذا الوقت.");

            // التأكد إن الـ Bus مش على رحلة تانية في نفس الوقت
            var busConflict = await _uow.Trips.HasBusConflictAsync(
                request.BusId, request.DepartureTime, request.ArrivalTime);
            if (busConflict)
                throw new ConflictException("الباص محجوز على رحلة أخرى في نفس الوقت.");

            var trip = new Trip
            {
                Id = Guid.NewGuid(),
                RouteId = request.RouteId,
                BusId = request.BusId,
                DriverId = request.DriverId,
                DepartureTime = request.DepartureTime,
                ArrivalTime = request.ArrivalTime,
                BasePrice = request.BasePrice,
                Status = TripStatus.Scheduled,
                AvailableSeats = bus.TotalSeats,
                CreatedDate = DateTime.UtcNow
            };

            await _uow.Trips.AddAsync(trip);

            // تعيين السائق كغير متاح
            driver.IsAvailable = false;
            driver.LastModifiedDate = DateTime.UtcNow;
            _uow.Drivers.Update(driver);

            await _uow.SaveChangesAsync();

            // إعادة الرحلة مع التفاصيل
            var created = await _uow.Trips.GetTripWithDetailsAsync(trip.Id) ?? trip;
            return _mapper.Map<TripDto>(created);
        }
    }
}

