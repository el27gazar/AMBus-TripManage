using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Dtos.Payment.Requests;
using AMBus.TripManage.Application.Features.BookingsF.Commands.ConfirmBookingCommands;
using AMBus.TripManage.Application.Features.PaymentsF.Commands.CancelPendingPayment;
using AMBus.TripManage.Application.Features.PaymentsF.Commands.ConfirmCashPayment;
using AMBus.TripManage.Application.Features.PaymentsF.Commands.InitiatePayment;
using AMBus.TripManage.Application.Features.PaymentsF.Commands.RefundPayment;
using AMBus.TripManage.Application.Features.PaymentsF.Queries.GetAllPayments;
using AMBus.TripManage.Application.Features.PaymentsF.Queries.GetPaymentByBooking;
using AMBus.TripManage.Application.Features.PaymentsF.Queries.GetPaymentById;
using AMBus.TripManage.Domain.Entites;
using AMBus.TripManage.Persistance.Service;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Text.Json;

namespace AMBus.TripManage.Api.Controllers
{
    [Authorize]
    public class PaymentsController : BaseController
    {
        // POST /api/payments/initiate  [User]
        [HttpPost("initiate")]
        [Authorize]
        public async Task<IActionResult> Initiate([FromBody] InitiateRequest req)
        {
            var result = await Mediator.Send(new InitiatePaymentCommand(
                req.BookingId, CurrentUserId, req.Method,
                req.PhoneNumber, req.Currency));

            return result.Success ? StatusCode(201, result) : StatusCode(402, result);
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

    [ApiController]
    [Route("api/payments/webhook")]
    public class PaymentWebhookController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;
        private readonly ISystemNotificationService _notif;

        public PaymentWebhookController(
            IUnitOfWork uow,
            IConfiguration config,
            ISystemNotificationService notif)
        { _uow = uow; _config = config; _notif = notif; }

        private readonly ILogger<PaymentWebhookController> _logger;

        public PaymentWebhookController(
            IUnitOfWork uow,
            IConfiguration config,
            ISystemNotificationService notif,
            ILogger<PaymentWebhookController> logger)
        { _uow = uow; _config = config; _notif = notif; _logger = logger; }

        [HttpPost("stripe")]
        public async Task<IActionResult> StripeCallback()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            if (!Request.Headers.TryGetValue("Stripe-Signature", out var signature))
                return BadRequest(new { error = "Missing Stripe-Signature header." });

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, signature, _config["Stripe:WebhookSecret"]);

                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                    if (session?.PaymentStatus != "paid") return Ok();

                    await Mediator.Send(new ConfirmBookingFromStripeCommand(
                        SessionId: session.Id,
                        PaymentIntentId: session.PaymentIntentId,
                        Metadata: session.Metadata));
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe signature/event parsing failed.");
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error while processing Stripe webhook.");
                return StatusCode(500, new { error = "Internal error processing webhook." });
            }
        }
    }
}