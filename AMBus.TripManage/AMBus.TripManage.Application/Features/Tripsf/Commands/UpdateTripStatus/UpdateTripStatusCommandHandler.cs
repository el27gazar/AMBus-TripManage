using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.TripDto;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Commands.UpdateTripStatus
{
    public class UpdateTripStatusCommandHandler
         : IRequestHandler<UpdateTripStatusCommand, TripDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ISystemNotificationService _notifications;

        public UpdateTripStatusCommandHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ISystemNotificationService notifications)
        {
            _uow = uow;
            _mapper = mapper;
            _notifications = notifications;
        }

        public async Task<TripDto> Handle(
            UpdateTripStatusCommand request,
            CancellationToken cancellationToken)
        {
            var trip = await _uow.Trips.GetTripWithDetailsAsync(request.TripId)
                ?? throw new NotFoundException(nameof(Trip), request.TripId);

            if (!Enum.TryParse<TripStatus>(request.Status, out var newStatus))
                throw new BusinessRuleException("حالة غير صحيحة.");

            // قواعد الانتقال
            var valid = (trip.Status, newStatus) switch
            {
                (TripStatus.Scheduled, TripStatus.InProgress) => true,
                (TripStatus.Scheduled, TripStatus.Cancelled) => true,
                (TripStatus.InProgress, TripStatus.Completed) => true,
                (TripStatus.InProgress, TripStatus.Cancelled) => true,
                _ => false
            };

            if (!valid)
                throw new BusinessRuleException(
                    $"لا يمكن الانتقال من {trip.Status} إلى {newStatus}.");

            trip.Status = newStatus;
            trip.LastModifiedDate = DateTime.UtcNow;

            // السائق يبقى متاح لو الرحلة خلصت أو اتلغت
            if (newStatus is TripStatus.Completed or TripStatus.Cancelled)
            {
                var driver = await _uow.Drivers.GetByIdAsync(trip.DriverId);
                if (driver is not null)
                {
                    driver.IsAvailable = true;
                    driver.LastModifiedDate = DateTime.UtcNow;
                    _uow.Drivers.Update(driver);
                }
            }

            _uow.Trips.Update(trip);
            await _uow.SaveChangesAsync();

            // ── إشعارات أوتوماتيكية ────────────────────
            switch (newStatus)
            {
                case TripStatus.InProgress:
                    await _notifications.NotifyTripStartedAsync(trip.Id);
                    break;

                case TripStatus.Completed:
                    await _notifications.NotifyTripCompletedAsync(trip.Id);
                    break;

                case TripStatus.Cancelled:
                    await _notifications.NotifyTripCancelledAsync(
                        trip.Id, "تم إلغاء الرحلة من قِبل الإدارة.");
                    break;
            }

            var updated = await _uow.Trips.GetTripWithDetailsAsync(trip.Id) ?? trip;
            return _mapper.Map<TripDto>(updated);
        }
    }
}
