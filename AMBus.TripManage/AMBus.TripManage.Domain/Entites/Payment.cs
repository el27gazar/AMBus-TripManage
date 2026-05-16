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
        Card,           // Paymob Credit/Debit
        VodafoneCash,
        OrangeCash,
        EtisalatCash,
        Fawry,
        Aman,
        Masary,
        Cash            // يدوي Admin
    }

    public enum PaymentStatus
    {
        Pending,
        PendingCustomerAction,  // فاتورة Fawry/Wallet — ينتظر العميل
        Paid,
        Failed,
        Refunded,
        Cancelled
    }

    public enum PaymentProvider
    {
        Paymob,
        Manual
    }

    public class Payment : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [MaxLength(10)]
        public string Currency { get; set; } = "EGP";

        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public PaymentProvider Provider { get; set; }

        // Paymob
        [MaxLength(100)] public string? PaymobOrderId { get; set; }
        [MaxLength(100)] public string? PaymobTransactionId { get; set; }
        [MaxLength(2000)] public string? PaymobPaymentToken { get; set; }

        // Fawry
        [MaxLength(100)] public string? FawryReferenceNumber { get; set; }

        // Wallet
        [MaxLength(20)] public string? WalletMsisdn { get; set; }
        [MaxLength(500)] public string? WalletRedirectUrl { get; set; }

        // Aman / Masary
        [MaxLength(100)] public string? OtcReferenceNumber { get; set; }

        // Generic
        [MaxLength(100)] public string? ExternalTransactionId { get; set; }

        public DateTime? PaidAt { get; set; }
        public DateTime? RefundedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }

        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = null!;
    }
}