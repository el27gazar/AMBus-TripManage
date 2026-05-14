using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.ResetPasswordCommands
{
    public class ResetPasswordCommandValidator:AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email Required.")
                .EmailAddress();

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("رمز إعادة التعيين مطلوب.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("كلمة المرور الجديدة مطلوبة.")
                .MinimumLength(8).WithMessage("لا تقل عن 8 أحرف.")
                .Matches("[A-Z]").WithMessage("يجب أن تحتوي على حرف كبير.")
                .Matches("[0-9]").WithMessage("يجب أن تحتوي على رقم.");
        }
    }
}
