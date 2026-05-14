using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.BookingDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.CreateBookingCommands
{
    public class CreateBookingCommand:IRequest<BookingDto>
    {
        public Guid UserId { get; set; }
        public Guid TripId { get; set; }
      public List<BookingSeatDto> Seats { get; set; }
    }
}
