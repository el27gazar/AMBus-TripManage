using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos;
using AMBus.TripManage.Application.Dtos.Booking;
using AMBus.TripManage.Application.Dtos.Requests;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AMBus.TripManage.Api.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UsersController(
            IUnitOfWork uow,
            IMapper mapper,
            UserManager<User> userManager)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
        }

       
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search,
            [FromQuery] bool? isActive,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var users = await _uow.Users.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
                users = users.Where(u =>
                    u.FullName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    u.Email!.Contains(search, StringComparison.OrdinalIgnoreCase));

            if (isActive.HasValue)
                users = users.Where(u => u.IsActive == isActive.Value);

            var total = users.Count();
            var totalPages = (int)Math.Ceiling(total / (double)pageSize);
            var items = users
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var dtos = new List<UserDto>();
            foreach (var user in items)
            {
                var roles = await _userManager.GetRolesAsync(user);
                dtos.Add(_mapper.Map<UserDto>(user) with
                {
                    Role = roles.FirstOrDefault() ?? "User"
                });
            }

            return Ok(new { items = dtos, totalCount = total, page, pageSize, totalPages });
        }

        
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMe()
        {
            var user = await _userManager.FindByIdAsync(CurrentUserId.ToString())
                ?? throw new NotFoundException(nameof(User), CurrentUserId);

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(_mapper.Map<UserDto>(user) with
            {
                Role = roles.FirstOrDefault() ?? "User"
            });
        }

        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!IsAdmin && id != CurrentUserId)
                throw new UnauthorizedException("You don't have permission.");

            var user = await _userManager.FindByIdAsync(id.ToString())
                ?? throw new NotFoundException(nameof(User), id);

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(_mapper.Map<UserDto>(user) with
            {
                Role = roles.FirstOrDefault() ?? "User"
            });
        }

       
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateUserRequest request)
        {
            if (!IsAdmin && id != CurrentUserId)
                throw new UnauthorizedException("You don't have permission.");

            var user = await _userManager.FindByIdAsync(id.ToString())
                ?? throw new NotFoundException(nameof(User), id);

            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;
            

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(_mapper.Map<UserDto>(user) with
            {
                Role = roles.FirstOrDefault() ?? "User"
            });
        }


        [HttpPut("{id:guid}/role")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRole(
            Guid id,
            [FromBody] UpdateUserRoleRequest request)
        {
            var allowedRoles = new[] { "Admin", "User", "Driver" };

            if (!allowedRoles.Contains(request.Role))
                return BadRequest(new
                {
                    message = $"Role must be one of: {string.Join(", ", allowedRoles)}"
                });

           
            if (id == CurrentUserId)
                throw new BusinessRuleException(
                    "You cannot change your own role.");

            var user = await _userManager.FindByIdAsync(id.ToString())
                ?? throw new NotFoundException(nameof(User), id);

            
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

           
            var addResult = await _userManager.AddToRoleAsync(user, request.Role);
            if (!addResult.Succeeded)
                return BadRequest(new
                {
                    errors = addResult.Errors.Select(e => e.Description)
                });

            return Ok(new { message = $"User role updated to {request.Role}." });
        }

        
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            if (id == CurrentUserId)
                throw new BusinessRuleException("You cannot deactivate your own account.");

            var user = await _userManager.FindByIdAsync(id.ToString())
                ?? throw new NotFoundException(nameof(User), id);

          
            user.IsActive = false;
            user.CreatedAt = DateTime.UtcNow;

           
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);

            await _userManager.UpdateAsync(user);

            return Ok(new { message = "Account deactivated. User can no longer sign in." });
        }

        
        [HttpPut("{id:guid}/activate")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Activate(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString())
                ?? throw new NotFoundException(nameof(User), id);

            user.IsActive = true;
            user.CreatedAt = DateTime.UtcNow;

            await _userManager.SetLockoutEndDateAsync(user, null);
            await _userManager.ResetAccessFailedCountAsync(user);

            await _userManager.UpdateAsync(user);

            return Ok(new { message = "Account activated. User can sign in now." });
        }

        
        [HttpGet("{id:guid}/bookings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetUserBookings(
            Guid id,
            [FromQuery] string? status,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (!IsAdmin && id != CurrentUserId)
                throw new UnauthorizedException("You don't have permission.");

            var (bookings, total) = await _uow.Bookings
                .GetUserBookingsPagedAsync(id, status, page, pageSize);

            return Ok(new
            {
                items = _mapper.Map<IEnumerable<BookingDto>>(bookings),
                totalCount = total,
                page,
                pageSize,
                totalPages = (int)Math.Ceiling(total / (double)pageSize)
            });
        }
    }
}
