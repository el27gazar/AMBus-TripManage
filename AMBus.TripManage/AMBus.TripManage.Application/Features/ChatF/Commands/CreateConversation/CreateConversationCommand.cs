using AMBus.TripManage.Application.Dtos.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Commands.CreateConversation
{
    public record CreateConversationCommand(
     Guid UserId,
     string? Subject,
     string FirstMessage
 ) : IRequest<ConversationDto>;
}
