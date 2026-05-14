using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos
{
   
        public record PagedResultDto<T>(
            List<T> Items,
            int TotalCount,
            int Page,
            int PageSize,
            int TotalPages
        );
    
}
