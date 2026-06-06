using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Commands.DeleteTrip
{
    public class DeleteTripCommandHandler : IRequestHandler<DeleteTripCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public DeleteTripCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Unit> Handle(
            DeleteTripCommand request,
            CancellationToken cancellationToken)
        {
            var trip = await _uow.Trips.GetByIdAsync(request.TripId)
                ?? throw new NotFoundException(nameof(Trip), request.TripId);

            if (trip.Status == TripStatus.InProgress)
                throw new BusinessRuleException("Can`t Remove Inprogress Trip");

            var hasBookings = await _uow.Bookings.HasActiveBookingsForTripAsync(request.TripId);
            if (hasBookings)
                throw new BusinessRuleException("Can`t Remove Trip Has Active Bookings ");

            trip.Driver.IsAvailable = true;
            trip.Bus.IsActive = true;
            _uow.Trips.Delete(trip);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
