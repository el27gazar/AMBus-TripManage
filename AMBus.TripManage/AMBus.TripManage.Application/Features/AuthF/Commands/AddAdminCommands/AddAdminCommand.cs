using AMBus.TripManage.Application.Dtos.AuthDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.AddAdminCommands
{
    public record AddAdminCommand(AddAdminRequest Request) : IRequest<AddAdminResult>;

    public record AddAdminResult(Guid UserId, string Email, string FullName);
}
