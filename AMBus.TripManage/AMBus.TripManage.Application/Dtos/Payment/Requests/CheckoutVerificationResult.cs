using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Payment.Requests
{
    public record CheckoutVerificationResult(
     bool Success,
     string? Status,            // "paid" أو "unpaid" أو "no_payment_required" أو "error"
     string? PaymentIntentId,
     Dictionary<string, string>? Metadata,
     string? Error
 );
}
