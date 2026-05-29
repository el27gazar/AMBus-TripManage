using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.BookingDto
{
    public class BookingDto {
     public Guid Id { get; set; }
     public Guid UserId { get; set; }
     public string UserName { get; set; }
     public Guid TripId { get; set; }
     public string TripSummary {  get; set; }
      public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string? QrCode {  get; set; }
       public  DateTime BookedAt { get; set; }
      public   List<BookingSeatDto> Seats { get; set; }

      public  PaymentDto? Payment {  get; set; }
      public   DateTime CreatedAt { get; set; }
     }
}
