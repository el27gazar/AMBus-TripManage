using AMBus.TripManage.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces.Repositories
{
    public interface IBookingRepository: IGenericRepository<Booking>
    {
        Task<Booking?> GetBookingWithDetailsAsync(Guid bookingId);
        Task<bool> IsSeatAlreadyBookedAsync(Guid seatId, Guid tripId);
        Task<bool> HasActiveBookingsForTripAsync(Guid tripId);
        Task<IEnumerable<Guid>> GetBookedSeatIdsForTripAsync(Guid tripId);

        Task<(IEnumerable<Booking> Items, int Total)> GetUserBookingsPagedAsync(
            Guid userId, string? status, int page, int pageSize);

        Task<(IEnumerable<Booking> Items, int Total)> GetAllBookingsPagedAsync(
            string? status, Guid? userId, Guid? tripId, int page, int pageSize);
        Task<bool> HasCompletedBookingForTripAsync(Guid userId, Guid tripId);

        Task<IEnumerable<Booking>> GetBookingsByTripAsync(Guid tripId);

    }
}
