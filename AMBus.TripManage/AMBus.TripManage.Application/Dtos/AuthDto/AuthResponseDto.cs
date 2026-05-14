using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.AuthDto
{
    public record AuthResponseDto(
     string? Token,
     DateTime? ExpiresAt,
     Guid UserId,
     string FullName,
     string Email,
     string Role,
     bool RequiresEmailConfirmation = false  
 );
}
