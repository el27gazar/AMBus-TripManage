using AMBus.TripManage.Application.Dtos.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Requests
{
    public class CreateBookingRequest
    {
        public Guid TripId { get; set; }
        public List<SeatRequest> Seats { get; set; } = new();
        public string PaymentMethod { get; set; } = "Card";
        public string? PhoneNumber { get; set; }
    }
    public class SeatRequest
    {
        public Guid SeatId { get; set; }
        public string PassengerName { get; set; } = string.Empty;
        public string? PassengerIdNumber { get; set; }
    }
}