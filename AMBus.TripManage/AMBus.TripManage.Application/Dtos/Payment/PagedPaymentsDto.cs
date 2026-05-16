using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Payment
{

    public record PagedPaymentsDto(
        List<PaymentDto> Items,
        int TotalCount,
        int Page,
        int PageSize,
        int TotalPages,
        PaymentSummaryDto Summary
    );
}
