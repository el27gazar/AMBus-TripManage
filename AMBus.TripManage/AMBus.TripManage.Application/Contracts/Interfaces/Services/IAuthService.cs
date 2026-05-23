using AMBus.TripManage.Application.Dtos.AuthDto;
using AMBus.TripManage.Application.Features.AuthF.Commands.RegisterCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   
    namespace AMBus.TripManage.Application.Contracts.Interfaces.Services
    {
        public interface IAuthService
        {
            Task<AuthResponseDto> RegisterAsync(RegisterRequestDto command);
            Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
            Task ChangePasswordAsync(Guid userId, string current, string newPass);
            Task GenerateForgotPasswordCodeAsync(string email);
            Task ResetPasswordAsync(string email, string otpCode, string newPassword);
            Task<bool> ConfirmEmailOtpAsync(string email, string code);
        }
    }

