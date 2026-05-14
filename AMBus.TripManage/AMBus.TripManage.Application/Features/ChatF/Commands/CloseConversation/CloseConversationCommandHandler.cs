using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Commands.CloseConversation
{
    public class CloseConversationCommandHandler
       : IRequestHandler<CloseConversationCommand, Unit>
    {
        private readonly IChatRepository _chatRepo;

        public CloseConversationCommandHandler(IChatRepository chatRepo)
            => _chatRepo = chatRepo;

        public async Task<Unit> Handle(
            CloseConversationCommand command,
            CancellationToken cancellationToken)
        {
            var conv = await _chatRepo
                .GetConversationAsync(command.ConversationId)
                ?? throw new NotFoundException(
                    nameof(ChatConversation), command.ConversationId);

            if (conv.Status == ConversationStatus.Closed)
                throw new BusinessRuleException("المحادثة مغلقة بالفعل.");

            conv.Status = ConversationStatus.Closed;
            conv.LastModifiedBy = command.AdminId.ToString();
            conv.LastModifiedDate = DateTime.UtcNow;

            await _chatRepo.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
