using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.AuthDto
{
    public record RegisterRequestDto(
      string FullName,
      string Email,
      string Password,
      [Required, Phone] string PhoneNumber
  ) :IRequest<AuthResponseDto>;
}
