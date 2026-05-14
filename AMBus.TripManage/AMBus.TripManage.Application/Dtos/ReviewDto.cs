using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos
{
    public record ReviewDto(
            Guid Id,
            Guid UserId,
            string UserName,
            Guid TripId,
            string TripSummary,
            int Rating,
            string? Comment,
            DateTime CreatedAt
        );
}
