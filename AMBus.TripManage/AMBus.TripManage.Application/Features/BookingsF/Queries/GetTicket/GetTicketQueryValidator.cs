using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Queries.GetTicket
{
    public class GetTicketQueryValidator : AbstractValidator<GetTicketQuery>
    {
        public GetTicketQueryValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty().WithMessage("الحجز مطلوب.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("المستخدم مطلوب.");
        }
    }
}
