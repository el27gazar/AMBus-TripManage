using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos
{
    public record PaymentDto(
          Guid Id,
          Guid BookingId,
          decimal Amount,
          string Method,
          string Status,
          string? TransactionId,
          DateTime? PaidAt,
          DateTime CreatedAt
      );
}
