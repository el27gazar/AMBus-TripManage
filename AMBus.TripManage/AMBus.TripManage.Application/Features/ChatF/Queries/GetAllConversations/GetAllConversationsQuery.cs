using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Queries.GetAllConversations
{
    public record GetAllConversationsQuery(
            string? Status,
            int Page = 1,
            int PageSize = 20
        ) : IRequest<PagedResultDto<ConversationDto>>;
}
