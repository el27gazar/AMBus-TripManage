using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.DriverDto
{
    public record DriverDto(
      Guid Id,
      Guid UserId,
      string FullName,
      string Email,
      string LicenseNumber,
      DateTime LicenseExpiry,
      string? EmergencyContact,
      bool IsAvailable,
      DateTime CreatedAt
  );
}
