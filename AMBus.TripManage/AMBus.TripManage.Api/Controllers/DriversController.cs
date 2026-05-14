using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos.DriverDto;
using AMBus.TripManage.Application.Dtos.Requests;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMBus.TripManage.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DriversController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DriversController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] bool? available)
        {
            var drivers = available == true
                ? await _uow.Drivers.GetAvailableDriversAsync()
                : await _uow.Drivers.GetAllAsync();

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(
            [FromBody] CreateDriverRequest request)
        {
            var user = await _uow.Users.GetByIdAsync(request.UserId)
                ?? throw new NotFoundException(nameof(User), request.UserId);

            var driver = new Driver
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                LicenseNumber = request.LicenseNumber,
                LicenseExpiry = request.LicenseExpiry,
                EmergencyContact = request.EmergencyContact,
                IsAvailable = true,
                CreatedDate = DateTime.UtcNow
            };

            await _uow.Drivers.AddAsync(driver);
            await _uow.SaveChangesAsync();

            var created = await _uow.Drivers.GetDriverWithUserAsync(driver.Id)!;
            return CreatedAtAction(
                nameof(GetById),
                new { id = driver.Id },
                _mapper.Map<DriverDto>(created));
        }

        [HttpPut("{id:guid}")]
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

        [HttpPut("{id:guid}/availability")]
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

            return Ok(new { message = "تم تحديث حالة السائق." });
        }

        [HttpDelete("{id:guid}")]
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
    }

}
