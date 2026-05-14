using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.ForgotPasswordCommands
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public ForgotPasswordCommandHandler(IEmailService emailService, IAuthService authService)
        {
            _authService = authService;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var token = await _authService.GenerateForgotPasswordTokenAsync(request.Email);
                

            await _emailService.SendEmailAsync(
                to: request.Email,
                subject: "إعادة تعيين كلمة المرور ",
                body: $"استخدم الرمز التالي لإعادة تعيين كلمة المرور: {token}");

            return Unit.Value;
        }
    }
}
