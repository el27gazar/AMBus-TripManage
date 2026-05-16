using AMBus.TripManage.Application.Dtos.Payment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.RefundPayment
{
    public record RefundPaymentCommand(
           Guid PaymentId,
           Guid AdminId,
           string Reason
       ) : IRequest<PaymentResultDto>;
}
