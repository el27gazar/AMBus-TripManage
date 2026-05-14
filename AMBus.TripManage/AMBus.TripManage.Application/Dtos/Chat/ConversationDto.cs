using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Chat
{
    public record ConversationDto(
         Guid Id,
         Guid UserId,
         string UserName,
         Guid? AdminId,
         string? AdminName,
         string Status,
         string? Subject,
         int UnreadCount,
         ChatMessageDto? LastMessage,
         DateTime CreatedDate
     );
    public record ChatMessageDto(
    Guid Id,
    Guid ConversationId,
    Guid SenderId,
    string SenderName,
    bool SenderIsAdmin,
    string Content,
    bool IsRead,
    DateTime? ReadAt,
    DateTime CreatedDate
);

    public record SendMessageDto(
        Guid ConversationId,
        string Content
    );

    public record CreateConversationDto(
        string? Subject,
        string FirstMessage
    );
}
