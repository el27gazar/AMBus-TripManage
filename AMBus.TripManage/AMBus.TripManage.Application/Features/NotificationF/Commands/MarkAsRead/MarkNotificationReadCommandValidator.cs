using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.NotificationF.Commands.MarkAsRead
{
    public class MarkNotificationReadCommandValidator
           : AbstractValidator<MarkNotificationReadCommand>
    {
        public MarkNotificationReadCommandValidator()
        {
            RuleFor(x => x.NotificationId)
                .NotEmpty().WithMessage("الإشعار مطلوب.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("المستخدم مطلوب.");
        }
    }
}
