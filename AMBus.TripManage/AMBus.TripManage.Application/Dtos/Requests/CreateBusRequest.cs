using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Requests
{
    public record CreateBusRequest(
       string PlateNumber,
       string Model,
       int TotalSeats,
       string Type);

    public record UpdateBusRequest(
        string Model,
        bool IsActive);
}
