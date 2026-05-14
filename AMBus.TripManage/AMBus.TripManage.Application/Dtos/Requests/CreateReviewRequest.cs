using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Requests
{
    public record CreateReviewRequest(
          Guid TripId,
          int Rating,
          string? Comment);

    public record UpdateReviewRequest(
        int Rating,
        string? Comment);
}
