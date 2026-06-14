using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Booking;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Queries.GetAllBookings
{
    public class GetAllBookingsQueryHandler
       : IRequestHandler<GetAllBookingsQuery, PagedResultDto<BookingDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllBookingsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<BookingDto>> Handle(
            GetAllBookingsQuery request,
            CancellationToken cancellationToken)
        {
            var (bookings, total) = await _uow.Bookings
                .GetAllBookingsPagedAsync(
                    request.Status,
                    request.UserId,
                    request.TripId,
                    request.Page,
                    request.PageSize);

            return new PagedResultDto<BookingDto> { 
                Items = _mapper.Map<List<BookingDto>>(bookings),
                TotalCount = total,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(total / (double)request.PageSize)
            };
        }
    }
}
