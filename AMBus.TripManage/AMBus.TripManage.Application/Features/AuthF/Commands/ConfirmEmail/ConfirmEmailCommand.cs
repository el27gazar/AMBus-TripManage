using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "رمز التأكيد مطلوب")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "رمز التأكيد يجب أن يتكون من 6 أرقام")]
        public string Token { get; set; } = string.Empty;
    }
}