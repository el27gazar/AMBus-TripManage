using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.NotificationF.Commands.DeleteNotification
{
    public record DeleteNotificationCommand(
        Guid NotificationId,
        Guid UserId
    ) : IRequest<Unit>;
}
