using AMBus.TripManage.Application.Dtos.AuthDto;
using AMBus.TripManage.Application.Dtos.Requests;
using AMBus.TripManage.Application.Features.AuthF.Commands.ChangePasswordCommands;
using AMBus.TripManage.Application.Features.AuthF.Commands.ConfirmEmail;
using AMBus.TripManage.Application.Features.AuthF.Commands.ForgotPasswordCommands;
using AMBus.TripManage.Application.Features.AuthF.Commands.Login;
using AMBus.TripManage.Application.Features.AuthF.Commands.RegisterCommands;
using AMBus.TripManage.Application.Features.AuthF.Commands.ResendConfirmation;
using AMBus.TripManage.Application.Features.AuthF.Commands.ResetPasswordCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMBus.TripManage.Api.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
       
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register(
            [FromBody] RegisterCommand command)
        {
            var result = await Mediator.Send(command);
            return Created(string.Empty, result);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(
            [FromBody] LoginCommand command)
        {
            var result = await Mediator.Send(command);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("jwt_token", result.Token, cookieOptions);
            return Ok(result);
        }

       
        [HttpPost("forgot-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ForgotPassword(
            [FromBody] ForgotPasswordCommand command)
        {
            await Mediator.Send(command);
            return Ok(new { message = "تم إرسال رمز إعادة التعيين." });
        }

        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword(
            [FromBody] ResetPasswordCommand command)
        {
            await Mediator.Send(command);
            return Ok(new { message = "تم تغيير كلمة المرور بنجاح." });
        }

        [HttpPost("change-password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePasswordRequest request)
        {
            await Mediator.Send(new ChangePasswordCommand {
                UserId = CurrentUserId,
                CurrentPassword = request.CurrentPassword,
                NewPassword = request.NewPassword});

            return Ok(new { message = "تم تغيير كلمة المرور." });
        }

        [HttpPost("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            var command = new ConfirmEmailCommand
            {
                Email = email,
                Token = token
            };

            await Mediator.Send(command);
            return Ok(new { message = "Email confirmed successfully. You can now login." });
        }
        [HttpPost("resend-confirmation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> ResendConfirmation([FromBody] ResendConfirmationCommand command)
        {
            await Mediator.Send(command);
            return Ok(new { message = "تم إرسال رابط التأكيد مرة أخرى. يرجى التحقق من بريدك الإلكتروني." });
        }
    }
}

