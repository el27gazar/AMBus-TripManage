using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Requests
{
    public class CreateDriverRequest
    {
        
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }

        public string LicenseNumber { get; set; } = string.Empty;
        public DateTime LicenseExpiry { get; set; }
        public string? EmergencyContact { get; set; }
    }
    public class UpdateDriverRequest
    {
        public string LicenseNumber { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public string? EmergencyContact { get; set; }
    }

    public class UpdateAvailabilityRequest
    {
        public bool IsAvailable { get; set; }
    }
}
