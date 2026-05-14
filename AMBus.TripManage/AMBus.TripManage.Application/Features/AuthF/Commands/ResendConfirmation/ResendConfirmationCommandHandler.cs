using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Exceptions;
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
        private readonly IConfiguration _config;

        public ResendConfirmationCommandHandler(
            UserManager<User> userManager,
            IEmailService emailService,
            IConfiguration config)
        {
            _userManager = userManager;
            _emailService = emailService;
            _config = config;
        }

        public async Task<bool> Handle(ResendConfirmationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                ?? throw new NotFoundException("User", request.Email);

            if (user.EmailConfirmed)
                throw new ConflictException("Email already confirmed.");

            
            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

         
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmationToken));

            
            var confirmationLink = $"https://localhost:7172/api/Auth/confirm-email?email={request.Email}&token={encodedToken}";

          
            await _emailService.SendConfirmationEmailAsync(user.Email, confirmationLink);

            return true;
        }
    }
}