using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.ResendConfirmation
{
    public class ResendConfirmationCommand : IRequest<bool>
    {
        public string Email { get; set; } = string.Empty;
    }
}
