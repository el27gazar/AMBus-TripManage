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
    public class NotificationRepository
           : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(AppDbContext ctx) : base(ctx) { }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(
            Guid userId, bool? isRead)
        {
            var query = _ctx.Notifications
                .Where(n => n.UserId == userId)
                .AsQueryable();

            if (isRead.HasValue)
                query = query.Where(n => n.IsRead == isRead.Value);

            return await query
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }
    }
}
