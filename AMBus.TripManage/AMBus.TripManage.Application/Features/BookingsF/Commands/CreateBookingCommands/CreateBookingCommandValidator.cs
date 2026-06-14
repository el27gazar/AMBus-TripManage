using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.BookingsF.Commands.CreateBookingCommands
{
    public class CreateBookingCommandValidator:AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.UserId)
                 .NotEmpty().WithMessage("المستخدم مطلوب.");

            RuleFor(x => x.TripId)
                .NotEmpty().WithMessage("الرحلة مطلوبة.");

            RuleFor(x => x.Seats)
                .NotEmpty()
                    .WithMessage("يجب اختيار مقعد واحد على الأقل.")
                .Must(s => s.Count <= 6)
                    .WithMessage("لا يمكن حجز أكثر من 6 مقاعد في مرة واحدة.")
                .Must(s => s.Select(x => x.SeatId).Distinct().Count() == s.Count)
                    .WithMessage("لا يمكن تكرار نفس المقعد.");

            RuleForEach(x => x.Seats).ChildRules(seat =>
            {
                seat.RuleFor(s => s.SeatId)
                    .NotEmpty().WithMessage("رقم المقعد مطلوب.");
            });
        }
    }
}
