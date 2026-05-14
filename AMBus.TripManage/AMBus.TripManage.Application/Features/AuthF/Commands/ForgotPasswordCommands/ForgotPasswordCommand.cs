using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.ForgotPasswordCommands
{
    public class ForgotPasswordCommand : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}
