using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.NotificationF.Commands.MarkAllAsRead
{
    public class MarkAllNotificationsReadCommandValidator
           : AbstractValidator<MarkAllNotificationsReadCommand>
    {
        public MarkAllNotificationsReadCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("المستخدم مطلوب.");
        }
    }
}
