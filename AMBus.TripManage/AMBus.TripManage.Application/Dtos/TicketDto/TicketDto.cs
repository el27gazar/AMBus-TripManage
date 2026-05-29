using AMBus.TripManage.Application.Dtos.BookingDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.TicketDto
{
    public class TicketDto
    {
        public Guid BookingId { get; set; }
        public string QrCode { get; set; } = string.Empty;
        public string FromCity { get; set; } = string.Empty;
        public string ToCity { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public string BusPlate { get; set; } = string.Empty;
        public string BusType { get; set; } = string.Empty;
        public List<BookingSeatDto> Passengers { get; set; } = new List<BookingSeatDto>();
    }
}
