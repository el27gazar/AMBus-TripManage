using AMBus.TripManage.Application.Dtos.TripDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.DashboardF.Queries.GetUpcomingTrips
{
    public record GetUpcomingTripsQuery(int Hours = 24) : IRequest<IEnumerable<TripDto>>;

}
