using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.ResetPasswordCommands
{
    public class ResetPasswordCommand:IRequest<Unit>
    {
       public string Email { get; set; }
       public string otpCode { get; set; }
       public string NewPassword { get; set; }
    }
}
