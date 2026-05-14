using AMBus.TripManage.Application.Contracts.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.NotificationF.Commands.MarkAllAsRead
{
    public class MarkAllNotificationsReadCommandHandler
         : IRequestHandler<MarkAllNotificationsReadCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public MarkAllNotificationsReadCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Unit> Handle(
            MarkAllNotificationsReadCommand request,
            CancellationToken cancellationToken)
        {
            var notifications = await _uow.Notifications
                .GetUserNotificationsAsync(request.UserId, isRead: false);

            foreach (var n in notifications)
            {
                n.IsRead = true;
                n.LastModifiedDate = DateTime.UtcNow;
                _uow.Notifications.Update(n);
            }

            await _uow.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
