using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Application.Dtos.Chat;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Commands.CreateConversation
{
    public class CreateConversationCommandHandler
           : IRequestHandler<CreateConversationCommand, ConversationDto>
    {
        private readonly IChatRepository _chatRepo;
        private readonly IMapper _mapper;

        public CreateConversationCommandHandler(
            IChatRepository chatRepo, IMapper mapper)
        {
            _chatRepo = chatRepo;
            _mapper = mapper;
        }

        public async Task<ConversationDto> Handle(
            CreateConversationCommand command,
            CancellationToken cancellationToken)
        {
            var existing = await _chatRepo.GetActiveConversationByUserAsync(command.UserId);

            if (existing is not null)
            {
                //if (!string.IsNullOrWhiteSpace(command.FirstMessage))
                //{
                //    var now = DateTime.UtcNow;
                //    var uid = command.UserId.ToString();

                //    var newMessage = new ChatMessage
                //    {
                //        Id = Guid.NewGuid(),
                //        ConversationId = existing.Id,
                //        SenderId = command.UserId,
                //        Content = command.FirstMessage.Trim(),
                //        IsRead = false,
                //        CreatedBy = uid,
                //        CreatedDate = now,
                //        LastModifiedBy = uid,
                //        LastModifiedDate = now
                //    };

                //    await _chatRepo.AddMessageAsync(newMessage);

                //    existing.LastModifiedDate = now;
                //    existing.LastModifiedBy = uid;

                //    await _chatRepo.SaveChangesAsync();
                //}

                return _mapper.Map<ConversationDto>(existing);
            }

            // ───── مفيش محادثة نشطة: نعمل محادثة جديدة مع الرسالة الأولى ─────
            var nowNew = DateTime.UtcNow;
            var uidNew = command.UserId.ToString();

            var conversation = new ChatConversation
            {
                Id = Guid.NewGuid(),
                UserId = command.UserId,
                Subject = command.Subject,
                Status = ConversationStatus.Open,
                CreatedBy = uidNew,
                CreatedDate = nowNew,
                LastModifiedBy = uidNew,
                LastModifiedDate = nowNew
            };

            var firstMsg = new ChatMessage
            {
                Id = Guid.NewGuid(),
                ConversationId = conversation.Id,
                SenderId = command.UserId,
                Content = command.FirstMessage,
                IsRead = false,
                CreatedBy = uidNew,
                CreatedDate = nowNew,
                LastModifiedBy = uidNew,
                LastModifiedDate = nowNew
            };

            conversation.Messages.Add(firstMsg);
            await _chatRepo.AddConversationAsync(conversation);
            await _chatRepo.SaveChangesAsync();

            return _mapper.Map<ConversationDto>(conversation);
        }
    }
}
