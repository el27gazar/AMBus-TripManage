using AMBus.TripManage.Application.Dtos.Payment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Commands.ConfirmCashPayment
{
    public record ConfirmCashPaymentCommand(
         Guid PaymentId,
         Guid AdminId
     ) : IRequest<PaymentResultDto>;
}
