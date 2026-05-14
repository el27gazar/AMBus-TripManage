using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.BookingDto
{
    public record BookingDto(
         Guid Id,
         Guid UserId,
         string UserName,
         Guid TripId,
         string TripSummary,
         decimal TotalPrice,
         string Status,
         string? QrCode,
         DateTime BookedAt,
         List<BookingSeatDto> Seats,
        
         PaymentDto? Payment,
         DateTime CreatedAt
     );
}
