using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.NotificationF.Commands.DeleteNotification
{
    public class DeleteNotificationCommandHandler
           : IRequestHandler<DeleteNotificationCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public DeleteNotificationCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Unit> Handle(
            DeleteNotificationCommand request,
            CancellationToken cancellationToken)
        {
            var notification = await _uow.Notifications.GetByIdAsync(request.NotificationId)
                ?? throw new NotFoundException(nameof(Notification), request.NotificationId);

            if (notification.UserId != request.UserId)
                throw new UnauthorizedException("You do not have the authority to edit this notice..");

            _uow.Notifications.Delete(notification);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
