using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Service
{
    public class PaymobPaymentService : IPaymentService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        private string ApiKey => _config["Paymob:ApiKey"]!;
        private string CardId => _config["Paymob:CardIntegrationId"]!;
        private string WalletId => _config["Paymob:WalletIntegrationId"]!;
        private string FawryId => _config["Paymob:FawryIntegrationId"]!;
        private string AmanId => _config["Paymob:AmanIntegrationId"]!;
        private string MasaryId => _config["Paymob:MasaryIntegrationId"]!;
        private string HmacSecret => _config["Paymob:HmacSecret"]!;
        private string Base => "https://accept.paymob.com/api";

        public PaymobPaymentService(HttpClient http, IConfiguration config)
        { _http = http; _config = config; }

        public async Task<PaymentInitResult> InitiatePaymentAsync(
            InitiatePaymentRequest req)
        {
            try
            {
                var auth = await GetAuthAsync();
                var orderId = await RegisterOrderAsync(auth, req);

                return req.Method switch
                {
                    "Card" => await CardAsync(auth, orderId, req),
                    "VodafoneCash" => await WalletAsync(auth, orderId, req, "VODAFONE"),
                    "OrangeCash" => await WalletAsync(auth, orderId, req, "ORANGE"),
                    "EtisalatCash" => await WalletAsync(auth, orderId, req, "ETISALAT"),
                    "Fawry" => await KioskAsync(auth, orderId, req, FawryId, 48),
                    "Aman" => await KioskAsync(auth, orderId, req, AmanId, 24),
                    "Masary" => await KioskAsync(auth, orderId, req, MasaryId, 24),
                    _ => new PaymentInitResult(false, "error", null, null,
                                        null, null, null, null, "طريقة غير مدعومة.")
                };
            }
            catch (Exception ex)
            {
                return new PaymentInitResult(false, "error", null, null,
                    null, null, null, null, ex.Message);
            }
        }

        private async Task<string> GetAuthAsync()
        {
            var r = await _http.PostAsJsonAsync($"{Base}/auth/tokens",
                new { api_key = ApiKey });
            r.EnsureSuccessStatusCode();
            var j = await r.Content.ReadFromJsonAsync<JsonElement>();
            return j.GetProperty("token").GetString()!;
        }

        private async Task<string> RegisterOrderAsync(
            string auth, InitiatePaymentRequest req)
        {
            var r = await _http.PostAsJsonAsync($"{Base}/ecommerce/orders", new
            {
                auth_token = auth,
                delivery_needed = false,
                amount_cents = (int)(req.Amount * 100),
                currency = req.Currency,
                merchant_order_id = req.BookingId.ToString(),
                items = Array.Empty<object>()
            });
            r.EnsureSuccessStatusCode();
            var j = await r.Content.ReadFromJsonAsync<JsonElement>();
            return j.GetProperty("id").GetInt64().ToString();
        }

        private async Task<string> GetKeyAsync(
            string auth, string orderId,
            InitiatePaymentRequest req, string integrationId)
        {
            var r = await _http.PostAsJsonAsync(
                $"{Base}/acceptance/payment_keys", new
                {
                    auth_token = auth,
                    amount_cents = (int)(req.Amount * 100),
                    expiration = 3600,
                    order_id = orderId,
                    billing_data = new
                    {
                        apartment = "NA",
                        email = req.CustomerEmail,
                        floor = "NA",
                        first_name = req.CustomerName.Split(' ').First(),
                        last_name = req.CustomerName.Split(' ').LastOrDefault() ?? "NA",
                        street = "NA",
                        building = "NA",
                        phone_number = req.PhoneNumber ?? "+201000000000",
                        shipping_method = "NA",
                        postal_code = "NA",
                        city = "Cairo",
                        country = "EG",
                        state = "NA"
                    },
                    currency = req.Currency,
                    integration_id = int.Parse(integrationId),
                    lock_order_when_paid = true
                });
            r.EnsureSuccessStatusCode();
            var j = await r.Content.ReadFromJsonAsync<JsonElement>();
            return j.GetProperty("token").GetString()!;
        }

        private async Task<PaymentInitResult> CardAsync(
            string auth, string orderId, InitiatePaymentRequest req)
        {
            var key = await GetKeyAsync(auth, orderId, req, CardId);
            return new PaymentInitResult(true, "iframe", key,
                null, null, orderId, null, null, null);
        }

        private async Task<PaymentInitResult> WalletAsync(
            string auth, string orderId,
            InitiatePaymentRequest req, string subtype)
        {
            var key = await GetKeyAsync(auth, orderId, req, WalletId);
            var r = await _http.PostAsJsonAsync(
                $"{Base}/acceptance/payments/pay", new
                {
                    source = new { identifier = req.PhoneNumber, subtype },
                    payment_token = key
                });
            r.EnsureSuccessStatusCode();
            var j = await r.Content.ReadFromJsonAsync<JsonElement>();
            var url = j.TryGetProperty("redirect_url", out var u) ? u.GetString() : null;
            var txId = j.TryGetProperty("id", out var i) ? i.GetInt64().ToString() : null;

            return new PaymentInitResult(true, "redirect", key, url,
                null, orderId, txId, DateTime.UtcNow.AddMinutes(10), null);
        }

        private async Task<PaymentInitResult> KioskAsync(
            string auth, string orderId,
            InitiatePaymentRequest req, string integrationId, int expiryHours)
        {
            var key = await GetKeyAsync(auth, orderId, req, integrationId);
            var r = await _http.PostAsJsonAsync(
                $"{Base}/acceptance/payments/pay", new
                {
                    source = new { identifier = "AGGREGATOR", subtype = "AGGREGATOR" },
                    payment_token = key
                });
            r.EnsureSuccessStatusCode();
            var j = await r.Content.ReadFromJsonAsync<JsonElement>();
            var ref1 = j.TryGetProperty("data", out var d) &&
                       d.TryGetProperty("bill_reference", out var br)
                       ? br.GetInt64().ToString() : null;

            return new PaymentInitResult(true, "show_reference", key,
                null, ref1, orderId, null,
                DateTime.UtcNow.AddHours(expiryHours), null);
        }

        public async Task<VerifyPaymentResult> VerifyPaymentAsync(
            string transactionId, string provider)
        {
            try
            {
                var r = await _http.GetAsync(
                    $"{Base}/acceptance/transactions/{transactionId}");
                if (!r.IsSuccessStatusCode)
                    return new VerifyPaymentResult(false, "error", null, "فشل التحقق.");

                var j = await r.Content.ReadFromJsonAsync<JsonElement>();
                var success = j.TryGetProperty("success", out var s) && s.GetBoolean();
                var pending = j.TryGetProperty("pending", out var p) && p.GetBoolean();

                return new VerifyPaymentResult(
                    success,
                    success ? "succeeded" : pending ? "pending" : "failed",
                    transactionId,
                    success ? null : "الدفع لم يكتمل.");
            }
            catch (Exception ex)
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
                var auth = await GetAuthAsync();
                var r = await _http.PostAsJsonAsync(
                    $"{Base}/acceptance/void_refund/refund", new
                    {
                        auth_token = auth,
                        transaction_id = transactionId,
                        amount_cents = (int)(amount * 100)
                    });

                if (!r.IsSuccessStatusCode)
                    return new RefundResult(false, null, "فشل الاسترداد.");

                var j = await r.Content.ReadFromJsonAsync<JsonElement>();
                var id = j.TryGetProperty("id", out var i)
                    ? i.GetInt64().ToString() : null;

                return new RefundResult(true, id, null);
            }
            catch (Exception ex)
            {
                return new RefundResult(false, null, ex.Message);
            }
        }

        public bool ValidateHmac(string hmac, Dictionary<string, string> data)
        {
            var keys = new[]
            {
                "amount_cents","created_at","currency","error_occured",
                "has_parent_transaction","id","integration_id","is_3d_secure",
                "is_auth","is_capture","is_refunded","is_standalone_payment",
                "is_voided","order","owner","pending","source_data.pan",
                "source_data.sub_type","source_data.type","success"
            };
            var concat = string.Concat(keys.Select(k =>
                data.TryGetValue(k, out var v) ? v : ""));

            using var hmacSha = new System.Security.Cryptography.HMACSHA512(
                System.Text.Encoding.UTF8.GetBytes(HmacSecret));

            var hash = Convert.ToHexString(
                hmacSha.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(concat)));

            return string.Equals(hash, hmac, StringComparison.OrdinalIgnoreCase);
        }
    }
}
