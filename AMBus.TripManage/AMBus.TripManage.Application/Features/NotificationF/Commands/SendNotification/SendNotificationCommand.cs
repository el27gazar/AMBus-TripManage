using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.NotificationF.Commands.SendNotification
{
    public record SendNotificationCommand(
        List<Guid> UserIds,
        string Type,       // "BookingConfirmed" | "TripCancelled" | "General" ...
        string Message
    ) : IRequest<Unit>;
}
