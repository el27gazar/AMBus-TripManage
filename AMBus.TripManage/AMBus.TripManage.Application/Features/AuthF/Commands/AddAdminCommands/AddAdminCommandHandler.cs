using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.AddAdminCommands
{
    public class AddAdminCommandHandler : IRequestHandler<AddAdminCommand, AddAdminResult>
    {
        private readonly UserManager<User> _userManager;

        public AddAdminCommandHandler(UserManager<User> userManager)
            => _userManager = userManager;

        public async Task<AddAdminResult> Handle(
            AddAdminCommand request, CancellationToken cancellationToken)
        {
            var req = request.Request;

            // تأكد إن الـ email مش موجود
            var existing = await _userManager.FindByEmailAsync(req.Email);
            if (existing != null)
                throw new ConflictException("البريد الإلكتروني مستخدم بالفعل.");

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = req.FullName,
                Email = req.Email,
                UserName = req.Email,
                PhoneNumber = req.PhoneNumber,
                EmailConfirmed = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var createResult = await _userManager.CreateAsync(user, req.Password);
            if (!createResult.Succeeded)
                throw new ValidationException(createResult.Errors
                    .Select(e => new ValidationFailure("", e.Description)));

            await _userManager.AddToRoleAsync(user, "Admin");

            return new AddAdminResult(user.Id, user.Email!, user.FullName);
        }
    }
}
