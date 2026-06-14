using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Booking;
using AMBus.TripManage.Application.Dtos.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.CreateBookingCommands
{
    public class CreateBookingCommand : IRequest<BookingWithPaymentDto>
    {
        public Guid UserId { get; set; }
        public Guid TripId { get; set; }
        public List<SeatRequest> Seats { get; set; } = new();
        public string PaymentMethod { get; set; } = "Card"; // default Card
        public string? PhoneNumber { get; set; }           // wallet payment method
    }
}
