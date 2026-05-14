using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly UserManager<User> _userManager;

        public ConfirmEmailCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                ?? throw new NotFoundException("User", request.Email);

            if (user.EmailConfirmed)
                throw new ConflictException("البريد الإلكتروني مفعل بالفعل.");

            var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", request.Token);

            if (!result)
            {
                throw new UnauthorizedException("رمز التأكيد غير صحيح أو انتهت صلاحيته.");
            }

            user.EmailConfirmed = true;
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                throw new Exception("حدث خطأ أثناء تحديث حالة الحساب.");
            }

            return true;
        }
    }
}