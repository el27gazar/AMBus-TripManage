using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace AMBus.TripManage.Application.Dtos.Payment
    {
        public class PaymentDto
        {
            public Guid Id { get; set; }
            public Guid BookingId { get; set; }
            public decimal Amount { get; set; }
            public string Currency { get; set; } = string.Empty;
            public string Method { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public string Provider { get; set; } = string.Empty;
            public string? PaymentToken { get; set; }        // StripeClientSecret
            public string? ReferenceNumber { get; set; }
            public string? ExternalTransactionId { get; set; }
            public DateTime? PaidAt { get; set; }
            public DateTime? ExpiresAt { get; set; }
            public string? CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
        }
    }

