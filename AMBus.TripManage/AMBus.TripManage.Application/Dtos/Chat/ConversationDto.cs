using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Chat
{
    public class ConversationDto
    {
      public Guid Id { get; set; }
      public  Guid UserId { get; set; }
     public string UserName { get; set; }
      public  Guid? AdminId { get; set; }
      public  string? AdminName { get; set; }
      public  string Status { get; set; }
       public string? Subject { get; set; }
      public int UnreadCount { get; set; }
        public ChatMessageDto? LastMessage { get; set; }
         public DateTime CreatedDate { get; set; }
     }
    public class ChatMessageDto
    {
      public Guid Id { get; set; }
       public Guid ConversationId { get; set; }
    public Guid SenderId { get; set; } 
    public string SenderName { get; set; }
    public bool SenderIsAdmin { get; set; }
    public string Content { get; set; }
    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime CreatedDate { get; set; }
    }

    public class SendMessageDto
    {
       public Guid ConversationId { get; set; }
       public string Content { get; set; }
    }

    public class CreateConversationDto
    {
       public string? Subject { get; set; }
        public string FirstMessage { get; set; }
    };
}
