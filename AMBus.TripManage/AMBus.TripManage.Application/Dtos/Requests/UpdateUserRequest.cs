using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Dtos.Requests
{
    public class UpdateUserRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
    }
    public class UpdateUserRoleRequest
    {
        public string Role { get; set; } = string.Empty;
    }

}
