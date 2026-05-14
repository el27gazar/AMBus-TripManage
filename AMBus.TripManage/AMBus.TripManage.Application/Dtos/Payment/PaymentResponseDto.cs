using AMBus.TripManage.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Payment
{
    public class PaymentResponseDto
    {
        public bool Success { get; set; }
        public Guid PaymentId { get; set; }
        public string? TransactionId { get; set; }
        public string? ReferenceNumber { get; set; }      // الرقم المرجعي للعميل
        public string? PaymentUrl { get; set; }           // رابط الدفع (لفوري أونلاين)
        public string? Instructions { get; set; }         // تعليمات الدفع
        public DateTime? ExpiresAt { get; set; }
        public string? Message { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
