using AMBus.TripManage.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces.Services
{
    public interface IPdfService
    {
        byte[] GenerateTripManifest(Trip trip, IEnumerable<Booking> bookings);
    }
}
