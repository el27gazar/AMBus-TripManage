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
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext ctx) : base(ctx) { }

        public async Task<Review?> GetReviewWithDetailsAsync(Guid reviewId)
            => await _ctx.Reviews
                .Include(r => r.User)
                .Include(r => r.Trip)
                    .ThenInclude(t => t.From)   // ✅
                .Include(r => r.Trip)
                    .ThenInclude(t => t.To)     // ✅
                .FirstOrDefaultAsync(r => r.Id == reviewId);

        public async Task<bool> HasUserReviewedTripAsync(Guid userId, Guid tripId)
            => await _ctx.Reviews.AnyAsync(r =>
                r.UserId == userId && r.TripId == tripId);

        public async Task<IEnumerable<Review>> GetUserReviewsAsync(Guid userId)
            => await _ctx.Reviews
                .Include(r => r.Trip)
                    .ThenInclude(t => t.From)   // ✅
                .Include(r => r.Trip)
                    .ThenInclude(t => t.To)     // ✅
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

        public async Task<(IEnumerable<Review> Items, int Total, double AvgRating)>
            GetTripReviewsPagedAsync(Guid tripId, int? rating, int page, int pageSize)
        {
            var query = _ctx.Reviews
                .Include(r => r.User)
                .Where(r => r.TripId == tripId)
                .AsQueryable();

            if (rating.HasValue)
                query = query.Where(r => r.Rating == rating.Value);

            var total = await query.CountAsync();
            var avg = total > 0 ? await query.AverageAsync(r => (double)r.Rating) : 0;

            var items = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total, avg);
        }
    }
}
