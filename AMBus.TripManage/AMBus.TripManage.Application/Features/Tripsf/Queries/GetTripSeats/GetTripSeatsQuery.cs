using AMBus.TripManage.Application.Dtos.SeatDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Queries.GetTripSeats
{
    public record GetTripSeatsQuery(Guid TripId) : IRequest<IEnumerable<SeatAvailabilityDto>>;
}
