using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Payment
{
    public record PaymentDto(
        Guid Id,
        Guid BookingId,
        decimal Amount,
        string Currency,
        string Method,
        string Status,
        string Provider,
        string? PaymentToken,
        string? WalletRedirectUrl,
        string? FawryReferenceNumber,
        string? OtcReferenceNumber,
        string? ExternalTransactionId,
        DateTime? PaidAt,
        DateTime? ExpiresAt,
        string? CreatedBy,
        DateTime CreatedDate
    );
}
