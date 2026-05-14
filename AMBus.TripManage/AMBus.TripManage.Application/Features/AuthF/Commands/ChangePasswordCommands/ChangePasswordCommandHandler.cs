using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.ChangePasswordCommands
{
    public class ChangePasswordCommandHandler:IRequestHandler<ChangePasswordCommand,Unit>
    {
        private readonly IAuthService _authService;

        public ChangePasswordCommandHandler(IAuthService authService)
        { _authService = authService; }

        public async Task<Unit> Handle(
            ChangePasswordCommand request,
            CancellationToken cancellationToken)
        {
            await _authService.ChangePasswordAsync(
                request.UserId,
                request.CurrentPassword,
                request.NewPassword);

            return Unit.Value;
        }
    }
}
