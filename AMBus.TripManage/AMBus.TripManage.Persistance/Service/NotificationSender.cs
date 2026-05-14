using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Domain.Entites;
using AMBus.TripManage.Persistance.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Service
{
    public class NotificationSender : INotificationSender
    {
        private readonly AppDbContext _ctx;
        private readonly UserManager<User> _userManager;

        public NotificationSender(
            AppDbContext ctx, UserManager<User> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        public async Task SendAsync(
            Guid userId, NotificationType type, string message)
        {
            await _ctx.Notifications.AddAsync(Build(userId, type, message));
            await _ctx.SaveChangesAsync();
        }

        public async Task SendBulkAsync(
            IEnumerable<Guid> userIds,
            NotificationType type,
            string message)
        {
            var list = userIds
                .Select(id => Build(id, type, message))
                .ToList();

            await _ctx.Notifications.AddRangeAsync(list);
            await _ctx.SaveChangesAsync();
        }

        public async Task SendToRoleAsync(
            string role, NotificationType type, string message)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);

            var list = users
                .Select(u => Build(u.Id, type, message))
                .ToList();

            await _ctx.Notifications.AddRangeAsync(list);
            await _ctx.SaveChangesAsync();
        }

        private static Notification Build(
            Guid userId, NotificationType type, string message)
            => new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Type = type,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
    }
}
