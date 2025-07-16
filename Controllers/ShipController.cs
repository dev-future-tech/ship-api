using Microsoft.AspNetCore.Mvc;
using Ships.Services;
using Ships.DTOs;

namespace Ships.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipController : ControllerBase
    {
        private readonly IShipService _shipService;
        public ShipController(IShipService shipService)
        {
            _shipService = shipService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ships = await _shipService.GetAllShipsAsync();
            return Ok(ships);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var ship = await _shipService.GetShipByIdAsync(id);
                return Ok(ship);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(ShipRequestDto shipDto)
        {
            await _shipService.AddShipAsync(shipDto);
            return CreatedAtAction(nameof(GetById), new { id = shipDto.Id }, shipDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ShipRequestDto shipDto)
        {
            try
            {
                await _shipService.UpdateShipAsync(id, shipDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _shipService.DeleteShipAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}