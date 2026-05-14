using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Chat;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Queries.GetAllConversations
{
    public class GetAllConversationsQueryHandler
            : IRequestHandler<GetAllConversationsQuery,
                              PagedResultDto<ConversationDto>>
    {
        private readonly IChatRepository _chatRepo;
        private readonly IMapper _mapper;

        public GetAllConversationsQueryHandler(
            IChatRepository chatRepo, IMapper mapper)
        {
            _chatRepo = chatRepo;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<ConversationDto>> Handle(
            GetAllConversationsQuery query,
            CancellationToken cancellationToken)
        {
            var convs = await _chatRepo
                .GetAllConversationsAsync(
                    query.Status, query.Page, query.PageSize);

            var total = await _chatRepo
                .GetTotalConversationsCountAsync(query.Status);

            return new PagedResultDto<ConversationDto>(
                Items: _mapper.Map<List<ConversationDto>>(convs),
                TotalCount: total,
                Page: query.Page,
                PageSize: query.PageSize,
                TotalPages: (int)Math.Ceiling(
                    total / (double)query.PageSize)
            );
        }
    }
}
