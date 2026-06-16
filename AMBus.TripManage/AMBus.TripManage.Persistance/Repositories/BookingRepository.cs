using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Domain.Entites;
using AMBus.TripManage.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext ctx) : base(ctx) { }

        public async Task<Booking?> GetBookingWithDetailsAsync(Guid bookingId)
            => await _ctx.Bookings
                .Include(b => b.User)
                .Include(b => b.Trip)
                    .ThenInclude(t => t.From)   // ✅ بدل Route
                .Include(b => b.Trip)
                    .ThenInclude(t => t.To)     // ✅ أضفنا To
                .Include(b => b.Trip)
                    .ThenInclude(t => t.Bus)
                .Include(b => b.BookingSeats)
                    .ThenInclude(bs => bs.Seat)
                .Include(b => b.Payment)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

        public async Task<bool> IsSeatAlreadyBookedAsync(Guid seatId, Guid tripId)
            => await _ctx.BookingSeats.AnyAsync(bs =>
                bs.SeatId == seatId &&
                bs.Booking.TripId == tripId &&
                bs.Booking.Status != BookingStatus.Cancelled);

        public async Task<bool> HasActiveBookingsForTripAsync(Guid tripId)
            => await _ctx.Bookings.AnyAsync(b =>
                b.TripId == tripId &&
                b.Status != BookingStatus.Cancelled);

        public async Task<bool> HasCompletedBookingForTripAsync(Guid userId, Guid tripId)
            => await _ctx.Bookings.AnyAsync(b =>
                b.UserId == userId &&
                b.TripId == tripId &&
                b.Status == BookingStatus.Completed);

        public async Task<IEnumerable<Guid>> GetBookedSeatIdsForTripAsync(Guid tripId)
            => await _ctx.BookingSeats
                .Where(bs =>
                    bs.Booking.TripId == tripId &&
                    bs.Booking.Status != BookingStatus.Cancelled)
                .Select(bs => bs.SeatId)
                .ToListAsync();

        public async Task<(IEnumerable<Booking> Items, int Total)>
            GetUserBookingsPagedAsync(Guid userId, string? status, int page, int pageSize)
        {
            var query = _ctx.Bookings
                .Include(b => b.Trip)
                    .ThenInclude(t => t.From)   // ✅
                .Include(b => b.Trip)
                    .ThenInclude(t => t.To)     // ✅
                .Include(b => b.BookingSeats)
                    .ThenInclude(bs => bs.Seat)
                .Include(b => b.Payment)
                .Where(b => b.UserId == userId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status) &&
                Enum.TryParse<BookingStatus>(status, out var s))
                query = query.Where(b => b.Status == s);

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(b => b.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }

        public async Task<(IEnumerable<Booking> Items, int Total)>
            GetAllBookingsPagedAsync(
                string? status, Guid? userId, Guid? tripId,
                int page, int pageSize)
        {
            var query = _ctx.Bookings
                .Include(b => b.User)
                .Include(b => b.Trip)
                    .ThenInclude(t => t.From)   // ✅
                .Include(b => b.Trip)
                    .ThenInclude(t => t.To)     // ✅
                .Include(b => b.BookingSeats)
                    .ThenInclude(bs => bs.Seat)
                .Include(b => b.Payment)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status) &&
                Enum.TryParse<BookingStatus>(status, out var s))
                query = query.Where(b => b.Status == s);

            if (userId.HasValue)
                query = query.Where(b => b.User.Id == userId.Value);

            if (tripId.HasValue)
                query = query.Where(b => b.Trip.Id == tripId.Value);

            var total = await query.CountAsync();
            var items = await query
                .OrderByDescending(b => b.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }
        public async Task<IEnumerable<Booking>> GetBookingsByTripAsync(Guid tripId)
            => await _ctx.Bookings
                .Include(b => b.BookingSeats)
                .ThenInclude(bs => bs.Seat)
                .Where(b => b.TripId == tripId && b.Status != BookingStatus.Cancelled)
                .ToListAsync();
    }
}
