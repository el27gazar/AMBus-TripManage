using AMBus.TripManage.Application.Dtos.TripDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.DriverF.Queries.GetDriverTrips
{
    public record GetDriverTripsQuery(Guid DriverId, string? Status)
        : IRequest<IEnumerable<TripDto>>;
}
