using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.NotificationF.Queries.GetMyNotifications
{
    public class GetMyNotificationsQueryValidator
            : AbstractValidator<GetMyNotificationsQuery>
    {
        public GetMyNotificationsQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("المستخدم مطلوب.");
        }
    }
}
