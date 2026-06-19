using AMBus.TripManage.Application.Dtos.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Commands.SendMessage
{
    public class SendMessageCommand : IRequest<ChatMessageDto>
    {
        public string ConversationId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Guid CurrentUserId { get; set; }
        public string CurrentUserName { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}
