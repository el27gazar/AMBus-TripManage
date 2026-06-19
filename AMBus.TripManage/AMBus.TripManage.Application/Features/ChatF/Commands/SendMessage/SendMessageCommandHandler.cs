using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Application.Dtos.Chat;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Commands.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, ChatMessageDto>
    {
        private readonly IChatRepository _chatRepo;

        
        public SendMessageCommandHandler(IChatRepository chatRepo)
        {
            _chatRepo = chatRepo;
        }

        public async Task<ChatMessageDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            // 1. التحقق من القيود والشروط
            if(request.Content==null)
                throw new ArgumentException("الرسالة فارغة.");

            if (request.Content.Length > 2000)
                throw new ArgumentException("الرسالة تتجاوز 2000 حرف.");

            if (!Guid.TryParse(request.ConversationId, out var convId))
                throw new ArgumentException("معرف المحادثة غير صحيح.");

            var conv = await _chatRepo.GetConversationAsync(convId);
            if (conv is null)
                throw new KeyNotFoundException("المحادثة غير موجودة.");

            // التحقق من الصلاحية (الأدمن يرسل لأي حد، المستخدم يرسل لمحادثته فقط)
            if (!request.IsAdmin && conv.UserId != request.CurrentUserId)
                throw new UnauthorizedAccessException("ليس لديك صلاحية لإرسال رسالة في هذه المحادثة.");

            if (conv.Status == ConversationStatus.Closed)
                throw new InvalidOperationException("المحادثة مغلقة ولا يمكن الإرسال.");

            var now = DateTime.UtcNow;

            var message = new ChatMessage
            {
                Id = Guid.NewGuid(),
                ConversationId = convId,
                SenderId = request.CurrentUserId,
                Content = request.Content.Trim(),
                IsRead = false,
                CreatedBy = request.CurrentUserId.ToString(),
                CreatedDate = now,
                LastModifiedBy = request.CurrentUserId.ToString(),
                LastModifiedDate = now
            };

            await _chatRepo.AddMessageAsync(message);

            // 3. تحديث بيانات المحادثة لو كان المستجيب هو الأدمن لأول مرة
            if (request.IsAdmin && conv.AdminId is null)
            {
                conv.AdminId = request.CurrentUserId;
                conv.Status = ConversationStatus.InProgress;
                conv.LastModifiedBy = request.CurrentUserId.ToString();
            }

            conv.LastModifiedDate = now;

            await _chatRepo.SaveChangesAsync();

            // 5. إرجاع الـ DTO المطلوب لعرضه في الواجهة
            return new ChatMessageDto
            {
                Id = message.Id,
                ConversationId = convId,
                SenderId = request.CurrentUserId,
                SenderName = request.CurrentUserName,
                SenderIsAdmin = request.IsAdmin,
                Content = message.Content,
                IsRead = false,
                ReadAt = null,
                CreatedDate = now
            };
        }
    }
}
