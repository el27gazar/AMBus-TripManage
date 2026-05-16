using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Dtos.Payment.Requests;
using AMBus.TripManage.Application.Features.PaymentsF.Commands.CancelPendingPayment;
using AMBus.TripManage.Application.Features.PaymentsF.Commands.ConfirmCashPayment;
using AMBus.TripManage.Application.Features.PaymentsF.Commands.ConfirmPayment;
using AMBus.TripManage.Application.Features.PaymentsF.Commands.InitiatePayment;
using AMBus.TripManage.Application.Features.PaymentsF.Commands.RefundPayment;
using AMBus.TripManage.Application.Features.PaymentsF.Queries.GetAllPayments;
using AMBus.TripManage.Application.Features.PaymentsF.Queries.GetPaymentByBooking;
using AMBus.TripManage.Application.Features.PaymentsF.Queries.GetPaymentById;
using AMBus.TripManage.Domain.Entites;
using AMBus.TripManage.Persistance.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AMBus.TripManage.Api.Controllers
{
    [Authorize]
    public class PaymentsController : BaseController
    {
        // POST /api/payments/initiate  [User]
        [HttpPost("initiate")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Initiate([FromBody] InitiateRequest req)
        {
            var result = await Mediator.Send(new InitiatePaymentCommand(
                req.BookingId, CurrentUserId, req.Method,
                req.PhoneNumber, req.Currency));

            return result.Success ? StatusCode(201, result) : StatusCode(402, result);
        }

        // POST /api/payments/confirm  [User]
        [HttpPost("confirm")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Confirm([FromBody] ConfirmRequest req)
        {
            var result = await Mediator.Send(
                new ConfirmPaymentCommand(req.TransactionId, CurrentUserId));
            return result.Success ? Ok(result) : StatusCode(402, result);
        }

        // POST /api/payments/{id}/confirm-cash  [Admin]
        [HttpPost("{id:guid}/confirm-cash")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ConfirmCash(Guid id)
        {
            var result = await Mediator.Send(
                new ConfirmCashPaymentCommand(id, CurrentUserId));
            return Ok(result);
        }

        // DELETE /api/payments/booking/{id}/pending  [User]
        [HttpDelete("booking/{bookingId:guid}/pending")]
        public async Task<IActionResult> CancelPending(Guid bookingId)
        {
            await Mediator.Send(
                new CancelPendingPaymentCommand(bookingId, CurrentUserId));
            return Ok(new { message = "تم الإلغاء. يمكنك اختيار طريقة دفع أخرى." });
        }

        // POST /api/payments/{id}/refund  [Admin]
        [HttpPost("{id:guid}/refund")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Refund(
            Guid id, [FromBody] RefundRequest req)
        {
            var result = await Mediator.Send(
                new RefundPaymentCommand(id, CurrentUserId, req.Reason));
            return Ok(result);
        }

        // GET /api/payments/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(
                new GetPaymentByIdQuery(id, CurrentUserId, IsAdmin));
            return Ok(result);
        }

        // GET /api/payments/booking/{id}
        [HttpGet("booking/{bookingId:guid}")]
        public async Task<IActionResult> GetByBooking(Guid bookingId)
        {
            var result = await Mediator.Send(
                new GetPaymentByBookingQuery(bookingId, CurrentUserId, IsAdmin));
            return result is null
                ? NotFound(new { message = "لا توجد دفعة لهذا الحجز بعد." })
                : Ok(result);
        }

        // GET /api/payments  [Admin]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? method,
            [FromQuery] string? status,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await Mediator.Send(
                new GetAllPaymentsQuery(method, status, from, to, page, pageSize));
            return Ok(result);
        }
    }

    // ── Webhook ───────────────────────────────────────────
    [ApiController]
    [Route("api/payments/webhook")]
    public class PaymentWebhookController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly PaymobPaymentService _paymob;
        private readonly ISystemNotificationService _notif;

        public PaymentWebhookController(
            IUnitOfWork uow,
            PaymobPaymentService paymob,
            ISystemNotificationService notif)
        { _uow = uow; _paymob = paymob; _notif = notif; }

        [HttpPost("paymob")]
        public async Task<IActionResult> Callback(
            [FromQuery] string hmac,
            [FromBody] JsonElement body)
        {
            var data = Flatten(body);
            if (!_paymob.ValidateHmac(hmac, data)) return Unauthorized();

            var success = data.GetValueOrDefault("success") == "true";
            var pending = data.GetValueOrDefault("pending") == "true";
            var txId = data.GetValueOrDefault("id");
            var orderId = data.GetValueOrDefault("order");

            if (string.IsNullOrEmpty(txId)) return Ok();

            var payment = await _uow.Payments.GetByOrderIdAsync(orderId ?? "");
            if (payment is null) return Ok();

            var now = DateTime.UtcNow;

            if (success && !pending)
            {
                payment.Status = PaymentStatus.Paid;
                payment.PaymobTransactionId = txId;
                payment.ExternalTransactionId = txId;
                payment.PaidAt = now;
                payment.LastModifiedBy = "Webhook";
                payment.LastModifiedDate = now;
                _uow.Payments.Update(payment);

                var booking = payment.Booking;
                booking.Status = BookingStatus.Confirmed;
                booking.LastModifiedBy = "Webhook";
                booking.LastModifiedDate = now;
                _uow.Bookings.Update(booking);

                await _uow.SaveChangesAsync();

                await _notif.NotifyPaymentReceivedAsync(booking.Id, payment.Amount);
                await _notif.NotifyBookingConfirmedAsync(booking.Id);
            }
            else if (!pending)
            {
                payment.Status = PaymentStatus.Failed;
                payment.LastModifiedBy = "Webhook";
                payment.LastModifiedDate = now;
                _uow.Payments.Update(payment);
                await _uow.SaveChangesAsync();
                await _notif.NotifyPaymentFailedAsync(payment.BookingId);
            }

            return Ok();
        }

        private static Dictionary<string, string> Flatten(
            JsonElement el, string prefix = "")
        {
            var r = new Dictionary<string, string>();
            foreach (var p in el.EnumerateObject())
            {
                var k = string.IsNullOrEmpty(prefix) ? p.Name : $"{prefix}.{p.Name}";
                if (p.Value.ValueKind == JsonValueKind.Object)
                    foreach (var s in Flatten(p.Value, k)) r[s.Key] = s.Value;
                else
                    r[k] = p.Value.ToString();
            }
            return r;
        }
    }
}
