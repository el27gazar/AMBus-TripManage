using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos.BusDto;
using AMBus.TripManage.Application.Dtos.Requests;
using AMBus.TripManage.Application.Dtos.SeatDto;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMBus.TripManage.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BusesController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BusesController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? type,
            [FromQuery] bool? isActive)
        {
            var buses = await _uow.Buses.GetAllAsync();

            if (!string.IsNullOrEmpty(type) &&
                Enum.TryParse<BusType>(type, true, out var busType))
                buses = buses.Where(b => b.Type == busType);

            if (isActive.HasValue)
                buses = buses.Where(b => b.IsActive == isActive.Value);

            return Ok(_mapper.Map<IEnumerable<BusDto>>(buses));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var bus = await _uow.Buses.GetBusWithSeatsAsync(id)
                ?? throw new NotFoundException(nameof(Bus), id);

            return Ok(_mapper.Map<BusDto>(bus));
        }

        [HttpGet("{id:guid}/seats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSeats(Guid id)
        {
            var bus = await _uow.Buses.GetBusWithSeatsAsync(id)
                ?? throw new NotFoundException(nameof(Bus), id);

            return Ok(_mapper.Map<IEnumerable<SeatDto>>(bus.Seats));
        }

        [HttpPost("create-bus")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(
            [FromBody] CreateBusRequest request)
        {
            if (!Enum.TryParse<BusType>(request.Type, true, out var busType))
                return BadRequest(new { message = "نوع الباص غير صحيح." });

            var bus = new Bus
            {
                Id = Guid.NewGuid(),
                PlateNumber = request.PlateNumber,
                Model = request.Model,
                TotalSeats = request.TotalSeats,
                Type = busType,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            bus.Seats = Enumerable.Range(1, request.TotalSeats)
                .Select(i => new Seat
                {
                    Id = Guid.NewGuid(),
                    SeatNumber = $"S{i:D2}",
                    IsAvailable = true,
                    BusId = bus.Id,
                    CreatedDate = DateTime.UtcNow
                }).ToList();

            await _uow.Buses.AddAsync(bus);
            await _uow.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = bus.Id },
                _mapper.Map<BusDto>(bus));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateBusRequest request)
        {
            var bus = await _uow.Buses.GetByIdAsync(id)
                ?? throw new NotFoundException(nameof(Bus), id);

            bus.Model = request.Model;
            bus.IsActive = request.IsActive;
            bus.LastModifiedDate = DateTime.UtcNow;

            _uow.Buses.Update(bus);
            await _uow.SaveChangesAsync();

            return Ok(_mapper.Map<BusDto>(bus));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var bus = await _uow.Buses.GetByIdAsync(id)
                ?? throw new NotFoundException(nameof(Bus), id);

            _uow.Buses.Delete(bus);
            await _uow.SaveChangesAsync();
            return NoContent();
        }
    }
}
