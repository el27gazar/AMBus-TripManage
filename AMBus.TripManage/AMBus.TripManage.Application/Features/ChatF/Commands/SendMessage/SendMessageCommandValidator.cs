using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Commands.SendMessage
{
    public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidator()
        {
            RuleFor(x => x.ConversationId)
                .NotEmpty().WithMessage("معرف المحادثة مطلوب.")
                .Must(id => Guid.TryParse(id, out _))
                    .WithMessage("معرف المحادثة غير صحيح.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("الرسالة فارغة.")
                .MaximumLength(2000).WithMessage("الرسالة تتجاوز 2000 حرف.");

            RuleFor(x => x.CurrentUserId)
                .NotEmpty().WithMessage("معرف المستخدم مطلوب.");

            RuleFor(x => x.CurrentUserName)
                .NotEmpty().WithMessage("اسم المستخدم مطلوب.");
        }
    }
}
