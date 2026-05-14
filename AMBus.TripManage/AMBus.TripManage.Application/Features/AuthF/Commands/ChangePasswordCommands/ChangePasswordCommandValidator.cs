using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.ChangePasswordCommands
{
    public class ChangePasswordCommandValidator:AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("المستخدم مطلوب.");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("كلمة المرور الحالية مطلوبة.");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                    .WithMessage("كلمة المرور الجديدة مطلوبة.")
                .MinimumLength(8)
                    .WithMessage("لا تقل عن 8 أحرف.")
                .Matches("[A-Z]")
                    .WithMessage("يجب أن تحتوي على حرف كبير.")
                .Matches("[0-9]")
                    .WithMessage("يجب أن تحتوي على رقم.")
                .NotEqual(x => x.CurrentPassword)
                    .WithMessage("كلمة المرور الجديدة يجب أن تختلف عن الحالية.");
        }
    }
}
