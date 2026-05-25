using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.ResetPasswordCommands
{
    public class ResetPasswordCommandHandler:IRequestHandler<ResetPasswordCommand,Unit>
    {
        private readonly IAuthService _authService;

        public ResetPasswordCommandHandler(IAuthService authService)
            => _authService = authService;

        public async Task<Unit> Handle(
            ResetPasswordCommand request,
            CancellationToken cancellationToken)
        {
            await _authService.ResetPasswordAsync(
                request.Email,
                request.otpCode,
                request.NewPassword);

            return Unit.Value;
        }
    }
}
