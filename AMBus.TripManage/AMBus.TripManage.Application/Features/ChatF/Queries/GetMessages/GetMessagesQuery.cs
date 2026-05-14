using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Queries.GetMessages
{
    public record GetMessagesQuery(
        Guid ConversationId,
        Guid UserId,
        bool IsAdmin = false,
        int Page = 1,
        int PageSize = 50
    ) : IRequest<PagedResultDto<ChatMessageDto>>;
}
