using AMBus.TripManage.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Payment
{
    public class PaymentRequestDto
    {
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        // بيانات العميل
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }

        // خاص بفودافون كاش
        public string? WalletNumber { get; set; }

        // رابط العودة بعد الدفع
        public string? CallbackUrl { get; set; }

        // عنوان الاستلام (للكاش عند الاستلام)
        public string? DeliveryAddress { get; set; }
    }

}
