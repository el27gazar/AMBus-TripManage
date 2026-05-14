using AMBus.TripManage.Application.Dtos.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Queries.GetMyConversations
{
    public record GetMyConversationsQuery(Guid UserId)
      : IRequest<IEnumerable<ConversationDto>>;
}
