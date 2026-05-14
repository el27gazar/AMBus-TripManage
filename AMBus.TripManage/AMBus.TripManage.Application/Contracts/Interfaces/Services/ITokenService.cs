using AMBus.TripManage.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Contracts.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user, string role);
        string GenerateRefreshToken();
    }
}
