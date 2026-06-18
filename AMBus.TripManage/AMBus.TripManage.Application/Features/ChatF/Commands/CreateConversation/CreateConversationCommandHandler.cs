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
            
            var existing = await _chatRepo.GetOpenConversationByUserAsync(command.UserId);
            if (existing is not null)
                return _mapper.Map<ConversationDto>(existing);

            var now = DateTime.UtcNow;
            var uid = command.UserId.ToString();

            var conversation = new ChatConversation
            {
                Id = Guid.NewGuid(),
                UserId = command.UserId,
                Subject = command.Subject,
                Status = ConversationStatus.Open,
                CreatedBy = uid,
                CreatedDate = now,
                LastModifiedBy = uid,
                LastModifiedDate = now
            };

            var firstMsg = new ChatMessage
            {
                Id = Guid.NewGuid(),
                ConversationId = conversation.Id,
                SenderId = command.UserId,
                Content = command.FirstMessage,
                IsRead = false,
                CreatedBy = uid,
                CreatedDate = now,
                LastModifiedBy = uid,
                LastModifiedDate = now
            };

            conversation.Messages.Add(firstMsg);

            await _chatRepo.AddConversationAsync(conversation);
            await _chatRepo.SaveChangesAsync();

            return _mapper.Map<ConversationDto>(conversation);
        }
    }
    }
