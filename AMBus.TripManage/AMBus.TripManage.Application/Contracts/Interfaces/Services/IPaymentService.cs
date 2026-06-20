using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Application.Dtos.Payment.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces.Services
{
   
        public interface IPaymentService
        {
        Task<CheckoutVerificationResult> VerifyCheckoutSessionAsync(string sessionId);
        Task<PaymentInitResult> InitiatePaymentAsync(InitiatePaymentRequest req);
            Task<VerifyPaymentResult> VerifyPaymentAsync(string transactionId, string provider);
            Task<RefundResult> RefundAsync(string transactionId, string provider, decimal amount, string reason);
            Task<CheckoutResult> CreateCheckoutSessionAsync(CreateCheckoutRequest req);   
    }

    public record CheckoutResult(bool Success, string? CheckoutUrl, string? SessionId, string? Error);
    public record CreateCheckoutRequest(
            decimal Amount, string Currency, string CustomerEmail,
            string CustomerName, Dictionary<string, string> Metadata);

    public record InitiatePaymentRequest(
       Guid BookingId,
       decimal Amount,
       string Currency,
       string Method,
       string? PhoneNumber,
       string CustomerName,
       string CustomerEmail
   );

    public record PaymentInitResult(
        bool Success,
        string Action,
        string? PaymentToken,
        string? RedirectUrl,
        string? ReferenceNumber,
        string? OrderId,
        string? TransactionId,
        DateTime? ExpiresAt,
        string? Error
    );

    public record VerifyPaymentResult(
        bool Success,
        string Status,
        string? TransactionId,
        string? Error
    );

    public record RefundResult(
        bool Success,
        string? RefundId,
        string? Error
    );
}
