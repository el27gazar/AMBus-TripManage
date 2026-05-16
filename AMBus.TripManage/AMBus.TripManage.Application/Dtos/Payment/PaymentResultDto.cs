using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Payment
{
    public record PaymentResultDto(
         bool Success,
         string Message,
         string? Action,
         PaymentDto Payment
     );
}
