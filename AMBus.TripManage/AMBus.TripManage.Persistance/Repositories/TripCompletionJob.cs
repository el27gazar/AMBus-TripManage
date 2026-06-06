using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Domain.Entites;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Repositories
{
    public class TripCompletionJob
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<TripCompletionJob> _logger;

        public TripCompletionJob(IUnitOfWork uow, ILogger<TripCompletionJob> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task ExecuteAsync()
        {
            var now = DateTime.UtcNow;

            var expiredTrips = await _uow.Trips.GetExpiredTripsAsync(now);

            if (!expiredTrips.Any()) return;

            foreach (var trip in expiredTrips)
            {
                trip.Status = TripStatus.Completed;
                trip.Driver.IsAvailable = true;
                trip.Bus.IsActive = true;

                _logger.LogInformation(
                    "Trip {TripId} completed. Driver {DriverId} and Bus {BusId} are now available.",
                    trip.Id, trip.DriverId, trip.BusId);
            }

            await _uow.SaveChangesAsync();
        }
    }
}
