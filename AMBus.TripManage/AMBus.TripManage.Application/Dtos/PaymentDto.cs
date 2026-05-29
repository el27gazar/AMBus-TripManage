using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
         public string? TransactionId { get; set; }
       public DateTime? PaidAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
