using AMBus.TripManage.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces.Repositories
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<Review?> GetReviewWithDetailsAsync(Guid reviewId);
        Task<bool> HasUserReviewedTripAsync(Guid userId, Guid tripId);
        Task<IEnumerable<Review>> GetUserReviewsAsync(Guid userId);

        Task<(IEnumerable<Review> Items, int Total, double AvgRating)>
        GetTripReviewsPagedAsync(Guid tripId, int? rating, int page, int pageSize);
        Task<(List<Review> Items, int TotalCount)> GetAllWithFiltersAsync(
    int? rating, Guid? tripId, Guid? userId,
    DateTime? from, DateTime? to, int page, int pageSize);
    }
}
