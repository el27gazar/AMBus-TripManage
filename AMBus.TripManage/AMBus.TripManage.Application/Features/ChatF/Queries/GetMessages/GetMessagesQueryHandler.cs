using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Chat;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Queries.GetMessages
{
    public class GetMessagesQueryHandler
            : IRequestHandler<GetMessagesQuery,
                              PagedResultDto<ChatMessageDto>>
    {
        private readonly IChatRepository _chatRepo;
        private readonly IMapper _mapper;

        public GetMessagesQueryHandler(
            IChatRepository chatRepo, IMapper mapper)
        {
            _chatRepo = chatRepo;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<ChatMessageDto>> Handle(
            GetMessagesQuery query,
            CancellationToken cancellationToken)
        {
            var conv = await _chatRepo
                .GetConversationAsync(query.ConversationId)
                ?? throw new NotFoundException(
                    nameof(ChatConversation), query.ConversationId);

           
            if (!query.IsAdmin && conv.UserId != query.UserId)
                throw new UnauthorizedException(
                    "ليس لديك صلاحية عرض هذه المحادثة.");

            var messages = await _chatRepo.GetMessagesAsync(
                query.ConversationId, query.Page, query.PageSize);

            var total = await _chatRepo
                .GetMessagesCountAsync(query.ConversationId);

            await _chatRepo.MarkMessagesAsReadAsync(
                query.ConversationId, query.UserId);
            await _chatRepo.SaveChangesAsync();

            return new PagedResultDto<ChatMessageDto> {
                Items = _mapper.Map<List<ChatMessageDto>>(messages),
                TotalCount = total,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalPages = (int)Math.Ceiling(
                    total / (double)query.PageSize)
            };
        }
    }
}
