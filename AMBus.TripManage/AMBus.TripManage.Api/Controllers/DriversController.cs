using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos.DriverDto;
using AMBus.TripManage.Application.Dtos.Requests;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Application.Features.DriverF.Queries;
using AMBus.TripManage.Application.Features.DriverF.Queries.GetDriverTrips;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AMBus.TripManage.Api.Controllers
{
   
    public class DriversController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public DriversController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager)
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] bool? available)
        {
            var drivers = available == true
                ? await _uow.Drivers.GetAvailableDriversAsync()
                : await _uow.Drivers.GetDriversAsync();

            return Ok(_mapper.Map<IEnumerable<DriverDto>>(drivers));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var driver = await _uow.Drivers.GetDriverWithUserAsync(id)
                ?? throw new NotFoundException(nameof(Driver), id);

            return Ok(_mapper.Map<DriverDto>(driver));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateDriverRequest request)
        {
            
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser is not null)
                return Conflict(new { message = $"'{request.Email}' already exists." });


            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
                EmailConfirmed = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                var errors = createResult.Errors.Select(e => e.Description);
                return BadRequest(new { message = "Failed to create user.", errors });
            }


            await _userManager.AddToRoleAsync(user, "Driver");

            var driver = new Driver
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                LicenseNumber = request.LicenseNumber,
                LicenseExpiry = request.LicenseExpiry,
                EmergencyContact = request.EmergencyContact,
                IsAvailable = true,
                CreatedDate = DateTime.UtcNow
            };

            await _uow.Drivers.AddAsync(driver);
            await _uow.SaveChangesAsync();

            var created = await _uow.Drivers.GetDriverWithUserAsync(driver.Id);
            return CreatedAtAction(
                nameof(GetById),
                new { id = driver.Id },
                _mapper.Map<DriverDto>(created));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateDriverRequest request)
        {
            var driver = await _uow.Drivers.GetByIdAsync(id)
                ?? throw new NotFoundException(nameof(Driver), id);

            driver.LicenseNumber = request.LicenseNumber;
            driver.LicenseExpiry = request.LicenseExpiry;
            driver.EmergencyContact = request.EmergencyContact;
            driver.LastModifiedDate = DateTime.UtcNow;

            _uow.Drivers.Update(driver);
            await _uow.SaveChangesAsync();

            var updated = await _uow.Drivers.GetDriverWithUserAsync(id)!;
            return Ok(_mapper.Map<DriverDto>(updated));
        }

        [HttpGet("{id:guid}/profile")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProfile(Guid id)
        {
            
            if (!IsAdmin)
            {
                
                var myDriver = await _uow.Drivers
                    .GetDriverByUserIdAsync(CurrentUserId);

                if (myDriver is null || myDriver.Id != id)
                    throw new UnauthorizedException(
                        "You can only view your own profile.");
            }

            var driver = await _uow.Drivers.GetDriverWithUserAsync(id)
                ?? throw new NotFoundException(nameof(Driver), id);

            
            var allTrips = await _uow.Trips.GetAllAsync();
            var driverTrips = allTrips
                .Where(t => t.DriverId == id)
                .ToList();

            var profile = new
            {
                id = driver.Id,
                fullName = driver.User.FullName,
                email = driver.User.Email,
                phoneNumber = driver.User.PhoneNumber,
                licenseNumber = driver.LicenseNumber,
                licenseExpiry = driver.LicenseExpiry,
                emergencyContact = driver.EmergencyContact,
                isAvailable = driver.IsAvailable,
                createdDate = driver.CreatedDate,

                // إحصائيات
                stats = new
                {
                    totalTrips = driverTrips.Count,
                    completedTrips = driverTrips.Count(t =>
                        t.Status == TripStatus.Completed),
                    cancelledTrips = driverTrips.Count(t =>
                        t.Status == TripStatus.Cancelled),
                    activeTrips = driverTrips.Count(t =>
                        t.Status == TripStatus.InProgress),
                    upcomingTrips = driverTrips.Count(t =>
                        t.Status == TripStatus.Scheduled &&
                        t.DepartureTime > DateTime.UtcNow)
                }
            };

            return Ok(profile);
        }

        [HttpPut("{id:guid}/availability")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAvailability(
            Guid id,
            [FromBody] UpdateAvailabilityRequest request)
        {
            var driver = await _uow.Drivers.GetByIdAsync(id)
                ?? throw new NotFoundException(nameof(Driver), id);

            driver.IsAvailable = request.IsAvailable;
            driver.LastModifiedDate = DateTime.UtcNow;

            _uow.Drivers.Update(driver);
            await _uow.SaveChangesAsync();

            return Ok(new { message = "Status Updated" });
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var driver = await _uow.Drivers.GetByIdAsync(id)
                ?? throw new NotFoundException(nameof(Driver), id);

            _uow.Drivers.Delete(driver);
            await _uow.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("my-trips")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMyTrips([FromQuery] string? status)
        {
            var driver = await _uow.Drivers.GetDriverByUserIdAsync(CurrentUserId)
                ?? throw new NotFoundException(nameof(Driver), CurrentUserId);

            var trips = await Mediator.Send(
                new GetDriverTripsQuery(driver.Id, status));

            return Ok(trips);
        }

        [HttpGet("my-trips/{tripId:guid}/manifest")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTripManifest(Guid tripId)
        {
            var pdfBytes = await Mediator.Send(
                new GetDriverTripManifestQuery(tripId, CurrentUserId));

            return File(pdfBytes, "application/pdf",
                $"manifest-trip-{tripId:N}.pdf");
        }

    }

}
