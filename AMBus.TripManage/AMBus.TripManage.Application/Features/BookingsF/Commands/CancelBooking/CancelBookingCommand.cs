using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.CancelBooking
{
    public class CancelBookingCommand:IRequest<Unit>   
    {
       public Guid BookingId { get; set; }
      public  Guid UserId { get; set; }
     
        public bool IsAdmin = false;
    }
}
