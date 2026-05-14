using AMBus.TripManage.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.NotificationF.Queries.GetMyNotifications
{
    public record GetMyNotificationsQuery(
        Guid UserId,
        bool? IsRead    // null = الكل | true = المقروء | false = الغير مقروء
    ) : IRequest<IEnumerable<NotificationDto>>;
}
