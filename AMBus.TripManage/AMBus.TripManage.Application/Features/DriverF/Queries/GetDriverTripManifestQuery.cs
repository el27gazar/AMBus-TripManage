using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.DriverF.Queries
{
    public record GetDriverTripManifestQuery(Guid TripId, Guid DriverId)
    : IRequest<byte[]>;
}
