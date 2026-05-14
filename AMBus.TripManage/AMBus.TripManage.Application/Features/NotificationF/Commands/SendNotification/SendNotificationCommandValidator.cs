using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.NotificationF.Commands.SendNotification
{
    public class SendNotificationCommandValidator
           : AbstractValidator<SendNotificationCommand>
    {
        private static readonly string[] AllowedTypes =
        {
            "BookingConfirmed", "TripReminder", "TripCancelled",
            "PaymentReceived",  "General"
        };

        public SendNotificationCommandValidator()
        {
            RuleFor(x => x.UserIds)
                .NotEmpty().WithMessage("يجب تحديد مستخدم واحد على الأقل.")
                .Must(ids => ids.All(id => id != Guid.Empty))
                    .WithMessage("معرفات المستخدمين غير صحيحة.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("نوع الإشعار مطلوب.")
                .Must(t => AllowedTypes.Contains(t))
                    .WithMessage($"النوع يجب أن يكون: {string.Join(", ", AllowedTypes)}.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("نص الإشعار مطلوب.")
                .MaximumLength(300).WithMessage("النص لا يتجاوز 300 حرف.");
        }
    }
}
