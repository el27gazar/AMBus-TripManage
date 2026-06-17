using AMBus.TripManage.Application.Dtos.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.CreateBookingCommands
{
    public record InitiateBookingPaymentCommand(
     Guid UserId,
     Guid TripId,
     List<SeatRequest> Seats,
     string PaymentMethod,
     string? PhoneNumber
 ) : IRequest<InitiateBookingPaymentResultDto>;

    public record InitiateBookingPaymentResultDto(
        bool Success,
        string Message,
        string Action,           // "redirect" أو "show_reference" (كاش)
        string? CheckoutUrl,
        Guid? TempBookingId       // مرجع مؤقت بس، مش موجود فعلياً في DB لو كارت
    );
}
