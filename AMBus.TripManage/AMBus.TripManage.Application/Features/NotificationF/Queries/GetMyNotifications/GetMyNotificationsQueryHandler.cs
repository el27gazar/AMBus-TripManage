using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.NotificationF.Queries.GetMyNotifications
{
    public class GetMyNotificationsQueryHandler
           : IRequestHandler<GetMyNotificationsQuery, IEnumerable<NotificationDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetMyNotificationsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationDto>> Handle(
            GetMyNotificationsQuery request,
            CancellationToken cancellationToken)
        {
            var notifications = await _uow.Notifications
                .GetUserNotificationsAsync(request.UserId, request.IsRead);

            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }
    }
}
