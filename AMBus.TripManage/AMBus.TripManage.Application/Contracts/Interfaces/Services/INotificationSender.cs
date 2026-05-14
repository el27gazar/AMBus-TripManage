using AMBus.TripManage.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces.Services
{
    public interface INotificationSender
    {
        // إرسال إشعار لمستخدم واحد
        Task SendAsync(
            Guid userId,
            NotificationType type,
            string message);

        // إرسال إشعار لمجموعة مستخدمين
        Task SendBulkAsync(
            IEnumerable<Guid> userIds,
            NotificationType type,
            string message);

        // إرسال إشعار لكل مستخدمي role معين
        Task SendToRoleAsync(
            string role,
            NotificationType type,
            string message);
    }
}
