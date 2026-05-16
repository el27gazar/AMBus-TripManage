using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Payment
{
    public record PaymentSummaryDto(
        int TotalCount,
        decimal TotalPaidAmount,
        decimal TotalRefundedAmount,
        int PaidCount,
        int FailedCount,
        int PendingCount,
        int RefundedCount
    );
}
