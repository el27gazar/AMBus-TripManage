using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.RouteDto
{
    public class RouteDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public List<StopDto> Stops { get; set; } = new List<StopDto>();
        public DateTime CreatedAt { get; set; }
    }
}
