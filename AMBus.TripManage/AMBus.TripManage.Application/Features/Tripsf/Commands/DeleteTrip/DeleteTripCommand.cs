using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Commands.DeleteTrip
{
    public record DeleteTripCommand(Guid TripId) : IRequest<Unit>;
}
