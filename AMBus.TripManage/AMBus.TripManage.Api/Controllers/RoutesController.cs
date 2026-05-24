using AMBus.TripManage.Application.Contracts.Interfaces;
using AMBus.TripManage.Application.Dtos.Requests;
using AMBus.TripManage.Application.Dtos.RouteDto;
using AMBus.TripManage.Application.Exceptions;
using AMBus.TripManage.Domain.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route = AMBus.TripManage.Domain.Entites.Route;



namespace AMBus.TripManage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RoutesController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

       
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] string? name)
        {
            var routes = await _uow.Routes.GetAllActiveRoutesAsync();

            var filtered = routes
                .Where(r => string.IsNullOrEmpty(name) ||
                            r.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            return Ok(_mapper.Map<IEnumerable<RouteDto>>(filtered));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var route = await _uow.Routes.GetRouteWithStopsAsync(id)
                ?? throw new NotFoundException(nameof(Route), id);

            return Ok(_mapper.Map<RouteDto>(route));
        }

       
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateRouteRequest request)
        {
            var route = new Route
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            await _uow.Routes.AddAsync(route);
            await _uow.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = route.Id },
                _mapper.Map<RouteDto>(route));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRouteRequest request)
        {
            var route = await _uow.Routes.GetByIdAsync(id)
                ?? throw new NotFoundException(nameof(Route), id);

            route.Name = request.Name;
            route.IsActive = request.IsActive;
            route.LastModifiedDate = DateTime.UtcNow;

            _uow.Routes.Update(route);
            await _uow.SaveChangesAsync();

            return Ok(_mapper.Map<RouteDto>(route));
        }

        
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var route = await _uow.Routes.GetByIdAsync(id)
                ?? throw new NotFoundException(nameof(Route), id);

            _uow.Routes.Delete(route);
            await _uow.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>إضافة محطة للمحافظة [Admin]</summary>
        [HttpPost("{routeId:guid}/stops")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddStop(Guid routeId, [FromBody] CreateStopRequest request)
        {
            var route = await _uow.Routes.GetByIdAsync(routeId)
                ?? throw new NotFoundException(nameof(Route), routeId);

            var stop = new Stop
            {
                Id = Guid.NewGuid(),
                RouteId = routeId,
                CityName = request.CityName,
                StationAddress = request.StationAddress,
                StopOrder = request.StopOrder,
                ArrivalOffsetMinutes = request.ArrivalOffsetMinutes,
                CreatedDate = DateTime.UtcNow
            };

            route.Stops.Add(stop);
            _uow.Routes.Update(route);
            await _uow.SaveChangesAsync();

            return Created(string.Empty, _mapper.Map<StopDto>(stop));
        }

        /// <summary>تعديل محطة [Admin]</summary>
        [HttpPut("{routeId:guid}/stops/{stopId:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStop(
            Guid routeId, Guid stopId, [FromBody] UpdateStopRequest request)
        {
            var route = await _uow.Routes.GetRouteWithStopsAsync(routeId)
                ?? throw new NotFoundException(nameof(Route), routeId);

            var stop = route.Stops.FirstOrDefault(s => s.Id == stopId)
                ?? throw new NotFoundException(nameof(Stop), stopId);

            stop.CityName = request.CityName;
            stop.StationAddress = request.StationAddress;
            stop.StopOrder = request.StopOrder;
            stop.ArrivalOffsetMinutes = request.ArrivalOffsetMinutes;
            stop.LastModifiedDate = DateTime.UtcNow;

            _uow.Routes.Update(route);
            await _uow.SaveChangesAsync();

            return Ok(_mapper.Map<StopDto>(stop));
        }

        /// <summary>حذف محطة [Admin]</summary>
        [HttpDelete("{routeId:guid}/stops/{stopId:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStop(Guid routeId, Guid stopId)
        {
            var route = await _uow.Routes.GetRouteWithStopsAsync(routeId)
                ?? throw new NotFoundException(nameof(Route), routeId);

            var stop = route.Stops.FirstOrDefault(s => s.Id == stopId)
                ?? throw new NotFoundException(nameof(Stop), stopId);

            route.Stops.Remove(stop);
            _uow.Routes.Update(route);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
    }
}