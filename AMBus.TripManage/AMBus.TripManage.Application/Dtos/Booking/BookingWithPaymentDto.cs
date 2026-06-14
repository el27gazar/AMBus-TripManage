using AMBus.TripManage.Application.Dtos.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Booking
{
    public record BookingWithPaymentDto(
    BookingDto Booking,
    PaymentResultDto Payment
);
}
