using AMBus.TripManage.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Domain.Entites
{
    public enum PaymentMethod
    {
        FawryCash = 1,      // دفع كاش من فروع فوري
        FawryWallet = 2,    // محفظة فوري
        VodafoneCash = 3,   // فودافون كاش
        InstaPay = 4,       // إنستا باي
       CashOnDelivery = 5, // كاش عند الاستلام (عند المكتب نفسه)
        BankTransfer = 6   // تحويل بنكي   
    }
    public enum PaymentStatus
    {
        Pending = 1,
        Completed = 2,
        Failed = 3,
        Expired = 4,
        Refunded = 5
    }

    public class Payment:AuditableEntity
    {
            public Guid Id { get; set; }

            [Column(TypeName = "decimal(10,2)")]
            public decimal Amount { get; set; }

            public PaymentMethod Method { get; set; }

            public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

            [MaxLength(100)]
            public string? TransactionId { get; set; } // from payment gateway

            

            // الرقم المرجعي اللي العميل يستخدمه للدفع (بيظهر للمستخدم)
            [MaxLength(50)]
            public string? ReferenceNumber { get; set; }

            // رقم محفظة فودافون كاش أو إنستا باي
            [MaxLength(20)]
            public string? WalletNumber { get; set; }

            // رقم تليفون العميل
            [MaxLength(20)]
            public string? CustomerMobile { get; set; }

            // تاريخ انتهاء صلاحية الدفع (فوري مثلاً 7 أيام)
            public DateTime? ExpiresAt { get; set; }

            // سبب الفشل لو الدفع مكملش
            [MaxLength(500)]
            public string? FailureReason { get; set; }

            // Webhook response data (كـ JSON)
            [MaxLength(2000)]
            public string? GatewayResponse { get; set; }

            [MaxLength(100)]
            public string? GatewayReferenceNumber { get; set; }

          
            public DateTime? PaidAt { get; set; } 
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public DateTime? UpdatedAt { get; set; }

            // FK (one-to-one with Booking)
            public Guid BookingId { get; set; }
            public Booking Booking { get; set; } = null!;
        
    }
}
