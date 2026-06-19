using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Features.AuthF.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public LogoutCommandHandler(IUnitOfWork uow)
            => _uow = uow;

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken ct)
        {
            var user = await _uow.Users.GetByIdAsync(request.UserId)
                ?? throw new NotFoundException(nameof(User), request.UserId);

            
            user.Tokens = null;

            _uow.Users.Update(user);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
 }
