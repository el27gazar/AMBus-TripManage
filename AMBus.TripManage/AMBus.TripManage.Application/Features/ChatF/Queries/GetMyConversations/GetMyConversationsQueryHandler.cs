using AMBus.TripManage.Application.Contracts.Interfaces.Repositories;
using AMBus.TripManage.Application.Dtos.Chat;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Queries.GetMyConversations
{
  
    public class GetMyConversationsQueryHandler
        : IRequestHandler<GetMyConversationsQuery,
                          IEnumerable<ConversationDto>>
    {
        private readonly IChatRepository _chatRepo;
        private readonly IMapper _mapper;

        public GetMyConversationsQueryHandler(
            IChatRepository chatRepo, IMapper mapper)
        {
            _chatRepo = chatRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConversationDto>> Handle(
            GetMyConversationsQuery query,
            CancellationToken cancellationToken)
        {
            var convs = await _chatRepo
                .GetUserConversationsAsync(query.UserId);

            return _mapper.Map<IEnumerable<ConversationDto>>(convs);
        }
    }
}
