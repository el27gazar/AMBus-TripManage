using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Commands.CreateConversation
{
    public class CreateConversationCommandValidator
         : AbstractValidator<CreateConversationCommand>
    {
        public CreateConversationCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("المستخدم مطلوب.");

            RuleFor(x => x.FirstMessage)
                .NotEmpty().WithMessage("الرسالة الأولى مطلوبة.")
                .MaximumLength(2000)
                    .WithMessage("الرسالة لا تتجاوز 2000 حرف.");

            RuleFor(x => x.Subject)
                .MaximumLength(200)
                    .WithMessage("الموضوع لا يتجاوز 200 حرف.")
                .When(x => x.Subject is not null);
        }
    }
}
