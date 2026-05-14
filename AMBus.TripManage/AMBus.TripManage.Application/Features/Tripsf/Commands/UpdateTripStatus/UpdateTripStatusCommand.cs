using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.TripDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Commands.UpdateTripStatus
{
    public record UpdateTripStatusCommand(
         Guid TripId,
         string Status   // "InProgress" | "Completed" | "Cancelled"
     ) : IRequest<TripDto>;
}
