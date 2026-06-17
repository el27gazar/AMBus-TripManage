using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.Logout
{
    public record LogoutCommand(Guid UserId) : IRequest<Unit>;

}
