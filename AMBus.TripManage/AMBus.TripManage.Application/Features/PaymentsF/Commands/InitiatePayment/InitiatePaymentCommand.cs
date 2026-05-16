using AMBus.TripManage.Application.Dtos.Payment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.InitiatePayment
{
    public record InitiatePaymentCommand(
           Guid BookingId,
           Guid UserId,
           string Method,
           string? PhoneNumber,
           string Currency = "EGP"
       ) : IRequest<PaymentResultDto>;
}
