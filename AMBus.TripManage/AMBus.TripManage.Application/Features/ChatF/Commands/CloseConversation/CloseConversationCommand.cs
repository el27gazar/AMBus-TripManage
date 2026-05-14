using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Commands.CloseConversation
{
    public record CloseConversationCommand(
          Guid ConversationId,
          Guid AdminId
      ) : IRequest<Unit>;
}
