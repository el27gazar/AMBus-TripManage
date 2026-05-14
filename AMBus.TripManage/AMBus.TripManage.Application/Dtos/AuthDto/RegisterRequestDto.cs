using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.AuthDto
{
    public record RegisterRequestDto(
      string FullName,
      string Email,
      string Password,
      string? PhoneNumber
  ):IRequest<AuthResponseDto>;
}
