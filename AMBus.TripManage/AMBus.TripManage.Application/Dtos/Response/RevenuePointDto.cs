using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Response
{
    public class RevenuePointDto
    {
        public string Period { get; set; } = string.Empty; // "Jan 2025" | "2025-08-01"
        public decimal Amount { get; set; }
        public int BookingsCount { get; set; }
    }
}
