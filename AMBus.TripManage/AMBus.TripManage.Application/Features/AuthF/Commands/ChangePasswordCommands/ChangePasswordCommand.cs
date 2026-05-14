using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.ChangePasswordCommands
{
    public class ChangePasswordCommand:IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; }
       public string NewPassword { get; set; }
    }
}
