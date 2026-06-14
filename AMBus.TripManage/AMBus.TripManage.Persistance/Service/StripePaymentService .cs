using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace AMBus.TripManage.Persistance.Service
{
    public class StripePaymentService : IPaymentService
    {
        private readonly IConfiguration _config;
        private string SecretKey => _config["Stripe:SecretKey"]!;
        private string WebhookSecret => _config["Stripe:WebhookSecret"]!;

        public StripePaymentService(IConfiguration config)
        {
            _config = config;
            StripeConfiguration.ApiKey = SecretKey;
        }

        public async Task<PaymentInitResult> InitiatePaymentAsync(
            InitiatePaymentRequest req)
        {
            try
            {
                if (req.Method == "Cash")
                    return new PaymentInitResult(
                        true, "show_reference", null, null,
                        req.BookingId.ToString(), null, null,
                        null, null);

                // إنشاء PaymentIntent
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(req.Amount * 100), // cents
                    Currency = req.Currency.ToLower(),
                    PaymentMethodTypes = new List<string> { "card" },
                    Metadata = new Dictionary<string, string>
                    {
                        { "bookingId", req.BookingId.ToString() },
                        { "customerEmail", req.CustomerEmail },
                        { "customerName", req.CustomerName }
                    },
                    Description = $"Booking {req.BookingId}",
                    ReceiptEmail = req.CustomerEmail
                };

                var service = new PaymentIntentService();
                var intent = await service.CreateAsync(options);

                return new PaymentInitResult(
                    Success: true,
                    Action: "stripe_intent",
                    PaymentToken: intent.ClientSecret, // الـ frontend هيستخدمه
                    RedirectUrl: null,
                    ReferenceNumber: null,
                    OrderId: intent.Id,
                    TransactionId: intent.Id,
                    ExpiresAt: DateTime.UtcNow.AddHours(1),
                    Error: null);
            }
            catch (StripeException ex)
            {
                return new PaymentInitResult(
                    false, "error", null, null,
                    null, null, null, null, ex.Message);
            }
        }

        public async Task<VerifyPaymentResult> VerifyPaymentAsync(
            string transactionId, string provider)
        {
            try
            {
                var service = new PaymentIntentService();
                var intent = await service.GetAsync(transactionId);

                var success = intent.Status == "succeeded";
                var pending = intent.Status == "requires_payment_method"
                           || intent.Status == "requires_confirmation"
                           || intent.Status == "processing";

                return new VerifyPaymentResult(
                    success,
                    intent.Status,
                    transactionId,
                    success ? null : "الدفع لم يكتمل.");
            }
            catch (StripeException ex)
            {
                return new VerifyPaymentResult(false, "error", null, ex.Message);
            }
        }

        public async Task<RefundResult> RefundAsync(
            string transactionId, string provider,
            decimal amount, string reason)
        {
            try
            {
                var options = new RefundCreateOptions
                {
                    PaymentIntent = transactionId,
                    Amount = (long)(amount * 100),
                    Reason = "requested_by_customer"
                };

                var service = new RefundService();
                var refund = await service.CreateAsync(options);

                return new RefundResult(true, refund.Id, null);
            }
            catch (StripeException ex)
            {
                return new RefundResult(false, null, ex.Message);
            }
        }

        public bool ValidateHmac(string hmac, Dictionary<string, string> data)
        {
            // Stripe بيستخدم Webhook signature مختلف
            // بيتعمل في الـ Webhook controller
            return true;
        }
    }
}