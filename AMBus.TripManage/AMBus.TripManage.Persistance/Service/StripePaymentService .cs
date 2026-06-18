using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
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
        public async Task<CheckoutResult> CreateCheckoutSessionAsync(CreateCheckoutRequest req)
        {
            try
            {
                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
            {
                new()
                {
                    PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                    {
                        Currency = req.Currency.ToLower(),
                        UnitAmount = (long)(req.Amount * 100),
                        ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "حجز رحلة - AMBus"
                        }
                    },
                    Quantity = 1
                }
            },
                    Mode = "payment",
                    SuccessUrl = $"{_config["App:FrontendUrl"]}/booking-success?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = $"{_config["App:FrontendUrl"]}/booking-cancelled",
                    CustomerEmail = req.CustomerEmail,
                    Metadata = req.Metadata,            
                    ExpiresAt = DateTime.UtcNow.AddMinutes(30) 
                };

                var service = new Stripe.Checkout.SessionService();
                var session = await service.CreateAsync(options);

                return new CheckoutResult(true, session.Url, session.Id, null);
            }
            catch (StripeException ex)
            {
                return new CheckoutResult(false, null, null, ex.Message);
            }
        }
        public async Task<PaymentInitResult> InitiatePaymentAsync(InitiatePaymentRequest req)
        {
            try
            {
                if (req.Method == "Cash")
                    return new PaymentInitResult(
                        true, "show_reference", null, null,
                        req.BookingId.ToString(), null, null,
                        null, null);

                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
            {
                new Stripe.Checkout.SessionLineItemOptions
                {
                    PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                    {
                        Currency = req.Currency.ToLower(),
                        UnitAmount = (long)(req.Amount * 100),
                        ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"حجز رحلة - {req.BookingId}"
                        }
                    },
                    Quantity = 1
                }
            },
                    Mode = "payment",
                    SuccessUrl = $"{_config["App:FrontendUrl"]}/booking-success?bookingId={req.BookingId}&session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = $"{_config["App:FrontendUrl"]}/booking-cancelled?bookingId={req.BookingId}",
                    CustomerEmail = req.CustomerEmail,
                    Metadata = new Dictionary<string, string>
            {
                { "bookingId", req.BookingId.ToString() },
                { "customerName", req.CustomerName }
            }
                };

                var service = new Stripe.Checkout.SessionService();
                var session = await service.CreateAsync(options);

                return new PaymentInitResult(
                    Success: true,
                    Action: "redirect",
                    PaymentToken: session.Id,            
                    RedirectUrl: session.Url,             
                    ReferenceNumber: null,
                    OrderId: session.PaymentIntentId,
                    TransactionId: session.Id,
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