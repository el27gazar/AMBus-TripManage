using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.NotificationF.Commands.SendNotification
{
    public class SendNotificationCommandHandler
        : IRequestHandler<SendNotificationCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public SendNotificationCommandHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Unit> Handle(
            SendNotificationCommand request,
            CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<NotificationType>(request.Type, out var type))
                throw new BusinessRuleException("Invalid Notify.");

            var notifications = request.UserIds.Select(userId => new Notification
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Type = type,
                Message = request.Message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            foreach (var n in notifications)
                await _uow.Notifications.AddAsync(n);

            await _uow.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
