using AMBus.TripManage.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces.Repositories
{
    public interface ITripRepository : IGenericRepository<Trip>
    {
        Task<Trip?> GetTripWithDetailsAsync(Guid tripId);

        Task<bool> HasBusConflictAsync(
            Guid busId, DateTime departure, DateTime arrival);

        Task<(IEnumerable<Trip> Items, int Total)> SearchTripsPagedAsync(
            string? fromCity, string? toCity, DateTime? date,
            int seats, int page, int pageSize);

        Task<IEnumerable<Trip>> GetUpcomingTripsAsync(DateTime from, DateTime until);

        Task<IEnumerable<Trip>> GetExpiredTripsAsync(DateTime now);
    }
}
