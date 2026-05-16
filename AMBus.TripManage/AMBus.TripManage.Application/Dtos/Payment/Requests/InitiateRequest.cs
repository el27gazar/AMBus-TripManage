using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Payment.Requests
{
    public record InitiateRequest(
         Guid BookingId,
         string Method,
         string? PhoneNumber = null,
         string Currency = "EGP");

    public record ConfirmRequest(string TransactionId);
    public record RefundRequest(string Reason);
}
