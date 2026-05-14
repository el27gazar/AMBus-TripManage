using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Commands.AssignAdmin
{
    public class AssignAdminCommandHandler
         : IRequestHandler<AssignAdminCommand, Unit>
    {
        private readonly IChatRepository _chatRepo;

        public AssignAdminCommandHandler(IChatRepository chatRepo)
            => _chatRepo = chatRepo;

        public async Task<Unit> Handle(
            AssignAdminCommand command,
            CancellationToken cancellationToken)
        {
            var conv = await _chatRepo
                .GetConversationAsync(command.ConversationId)
                ?? throw new NotFoundException(
                    nameof(ChatConversation), command.ConversationId);

            if (conv.Status == ConversationStatus.Closed)
                throw new BusinessRuleException("المحادثة مغلقة.");

            conv.AdminId = command.AdminId;
            conv.Status = ConversationStatus.InProgress;
            conv.LastModifiedBy = command.AdminId.ToString();
            conv.LastModifiedDate = DateTime.UtcNow;

            await _chatRepo.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
