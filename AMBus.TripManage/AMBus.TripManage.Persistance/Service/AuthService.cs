using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using AMBus.TripManage.Application.Dtos.AuthDto;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Application.Features.AuthF.Commands.RegisterCommands;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public AuthService(
            UserManager<User> userManager,
            ITokenService tokenService,
            IMapper mapper, IEmailService emailService, IConfiguration config)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _emailService = emailService;
            _config = config;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto command)
        {
            var exists = await _userManager.FindByEmailAsync(command.Email);
            if (exists is not null)
                throw new ConflictException($"البريد الإلكتروني '{command.Email}' مسجل بالفعل.");

            var user = _mapper.Map<User>(command);

            if (string.IsNullOrEmpty(user.UserName))
                user.UserName = command.Email;

            user.EmailConfirmed = false;

            var result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                var failures = result.Errors.Select(e =>
                    new FluentValidation.Results.ValidationFailure(e.Code, e.Description)).ToList();
                throw new ValidationException(failures);
            }

            await _userManager.AddToRoleAsync(user, "User");

            var otpCode = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");


            await _emailService.SendEmailAsync(user.Email, "رمز التأكيد الخاص بك", $"رمز التأكيد هو: {otpCode}");
            return new AuthResponseDto(
                Token: null,
                ExpiresAt: null,
                UserId: user.Id,
                FullName: user.FullName,
                Email: user.Email!,
                Role: "User",
                RequiresEmailConfirmation: true
            );
        }
        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email)
                ?? throw new UnauthorizedException("بيانات الدخول غير صحيحة.");

            if (!user.EmailConfirmed)
                throw new UnauthorizedException("Please confirm your email address before logging in. Check your inbox for the confirmation link.");

            if (!await _userManager.CheckPasswordAsync(user, loginRequestDto.Password))
                throw new UnauthorizedException("بيانات الدخول غير صحيحة.");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";

            return new AuthResponseDto(
                Token: _tokenService.GenerateToken(user, role),
                ExpiresAt: DateTime.UtcNow.AddDays(7),
                UserId: user.Id,
                FullName: user.FullName,
                Email: user.Email!,
                Role: role,
                RequiresEmailConfirmation: false
            );
        }
        public async Task ChangePasswordAsync(
            Guid userId, string current, string newPass)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new NotFoundException(nameof(User), userId);

            var result = await _userManager.ChangePasswordAsync(
                user, current, newPass);

            if (!result.Succeeded)
                throw new ValidationException(
                    result.Errors.Select(e =>
                        new FluentValidation.Results.ValidationFailure(
                            "Password", e.Description)));
        }

        public async Task<string> GenerateForgotPasswordTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new NotFoundException("User", email);

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task ResetPasswordAsync(
            string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new NotFoundException("User", email);

            var result = await _userManager.ResetPasswordAsync(
                user, token, newPassword);

            if (!result.Succeeded)
                throw new ValidationException(
                    result.Errors.Select(e =>
                        new FluentValidation.Results.ValidationFailure(
                            "Password", e.Description)));
        }

        public async Task<bool> ConfirmEmailOtpAsync(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

           
            var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", code);

            if (result)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return true;
            }

            return false;
        }
    }
}
