using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Application.Templates;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.ResendConfirmation
{
    public class ResendConfirmationCommandHandler : IRequestHandler<ResendConfirmationCommand, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

        public ResendConfirmationCommandHandler(
            UserManager<User> userManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<bool> Handle(ResendConfirmationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                ?? throw new NotFoundException("User", request.Email);

            if (user.EmailConfirmed)
                throw new ConflictException("Email already confirmed.");

            // OTP بدل link
            var otpCode = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            await _emailService.SendEmailAsync(
                user.Email!,
                "تأكيد البريد الإلكتروني",
                EmailTemplates.ConfirmEmail(user.FullName, otpCode));

            return true;
        }
    }
}