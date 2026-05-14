using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Commands.AssignAdmin
{
    public record AssignAdminCommand(
        Guid ConversationId,
        Guid AdminId
    ) : IRequest<Unit>;

}
