using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.BusDto
{
    public record BusDto(
        Guid Id,
        string PlateNumber,
        string Model,
        int TotalSeats,
        string Type,
        bool IsActive,
        DateTime CreatedAt
    );
}
