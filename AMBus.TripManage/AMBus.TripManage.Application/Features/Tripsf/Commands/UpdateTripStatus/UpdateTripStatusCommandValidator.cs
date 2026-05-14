using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.Tripsf.Commands.UpdateTripStatus
{
    public class UpdateTripStatusCommandValidator : AbstractValidator<UpdateTripStatusCommand>
    {
        private static readonly string[] Allowed = { "InProgress", "Completed", "Cancelled" };

        public UpdateTripStatusCommandValidator()
        {
            RuleFor(x => x.TripId)
                .NotEmpty().WithMessage("الرحلة مطلوبة.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("الحالة مطلوبة.")
                .Must(s => Allowed.Contains(s))
                    .WithMessage($"الحالة يجب أن تكون: {string.Join(", ", Allowed)}.");
        }
    }
}
