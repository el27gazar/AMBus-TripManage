using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.NotificationF.Commands.MarkAsRead
{
    public class MarkNotificationReadCommandHandler
        : IRequestHandler<MarkNotificationReadCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public MarkNotificationReadCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Unit> Handle(
            MarkNotificationReadCommand request,
            CancellationToken cancellationToken)
        {
            var notification = await _uow.Notifications.GetByIdAsync(request.NotificationId)
                ?? throw new NotFoundException(nameof(Notification), request.NotificationId);

            if (notification.UserId != request.UserId)
                throw new UnauthorizedException("ليس لديك صلاحية تعديل هذا الإشعار.");

            notification.IsRead = true;
            notification.LastModifiedDate = DateTime.UtcNow;

            _uow.Notifications.Update(notification);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
