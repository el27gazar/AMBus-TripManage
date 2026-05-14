using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.NotificationF.Commands.MarkAsRead
{
    public record MarkNotificationReadCommand(
        Guid NotificationId,
        Guid UserId
    ) : IRequest<Unit>;
}
