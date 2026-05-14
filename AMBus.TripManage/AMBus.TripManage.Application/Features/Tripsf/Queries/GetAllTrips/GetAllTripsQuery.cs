using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.TripDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Queries.GetAllTrips
{
    public record GetAllTripsQuery(
         string? FromCity,
         string? ToCity,
         DateTime? Date,
         int Seats = 1,
         int Page = 1,
         int PageSize = 20
     ) : IRequest<PagedResultDto<TripDto>>;
}
