using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Commands.CloseConversation
{
    public class CloseConversationCommandValidator
         : AbstractValidator<CloseConversationCommand>
    {
        public CloseConversationCommandValidator()
        {
            RuleFor(x => x.ConversationId).NotEmpty();
            RuleFor(x => x.AdminId).NotEmpty();
        }
    }
}
