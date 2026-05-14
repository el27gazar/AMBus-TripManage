using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.ChatF.Commands.AssignAdmin
{
    public class AssignAdminCommandValidator
         : AbstractValidator<AssignAdminCommand>
    {
        public AssignAdminCommandValidator()
        {
            RuleFor(x => x.ConversationId).NotEmpty();
            RuleFor(x => x.AdminId).NotEmpty();
        }
    }
}
