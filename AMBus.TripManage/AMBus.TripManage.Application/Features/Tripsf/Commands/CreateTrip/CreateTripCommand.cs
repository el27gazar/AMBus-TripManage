using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.TripDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Commands.CreateTrip
{


    public class CreateTripCommand : IRequest<TripDto>
    {
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public Guid BusId { get; set; }
        public Guid DriverId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal BasePrice { get; set; }
    }
}

