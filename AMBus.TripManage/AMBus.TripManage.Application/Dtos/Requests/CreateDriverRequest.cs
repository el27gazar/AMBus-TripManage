using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Requests
{
    public record CreateDriverRequest(
          Guid UserId,
          string LicenseNumber,
          DateTime LicenseExpiry,
          string? EmergencyContact);

    public record UpdateDriverRequest(
        string LicenseNumber,
        DateTime LicenseExpiry,
        string? EmergencyContact);

    public record UpdateAvailabilityRequest(bool IsAvailable);
}
