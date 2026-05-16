using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.PaymentsF.Queries.GetAllPayments
{
    public class GetAllPaymentsQueryValidator
           : AbstractValidator<GetAllPaymentsQuery>
    {
        public GetAllPaymentsQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
            RuleFor(x => x.To)
                .GreaterThan(x => x.From)
                .When(x => x.From.HasValue && x.To.HasValue)
                .WithMessage("تاريخ النهاية بعد البداية.");
        }
    }
}
