using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.ConfirmStripePayment
{
    public record ConfirmStripePaymentCommand(
          string SessionId,
          string PaymentIntentId,
          Dictionary<string, string> Metadata) : IRequest<Unit>;

    public class ConfirmStripePaymentRequest
    {
        public string SessionId { get; set; }
        public string PaymentIntentId { get; set; }
        public string Metadata { get; set; }
    }
}