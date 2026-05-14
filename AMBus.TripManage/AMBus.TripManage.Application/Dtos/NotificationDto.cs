using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos
{
    public record NotificationDto(
       Guid Id,
       string Type,
       string Message,
       bool IsRead,
       DateTime CreatedAt
   );
}
