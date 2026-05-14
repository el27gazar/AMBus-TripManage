using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.TripDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Commands.UpdateTrip
{
    public record UpdateTripCommand(
        Guid TripId,
        Guid DriverId,
        DateTime DepartureTime,
        DateTime ArrivalTime,
        decimal BasePrice
    ) : IRequest<TripDto>;
}
