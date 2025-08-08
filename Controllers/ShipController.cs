using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySecureWebApi.DTOs;
using MySecureWebApi.Services;

namespace MySecureWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipController(IShipService shipService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var ships = await shipService.GetAllShipsAsync();
        return Ok(ships);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var ship = await shipService.GetShipByIdAsync(id);
            return Ok(ship);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add([FromBody] ShipRequestDto shipDto)
    {
        await shipService.AddShipAsync(shipDto);
        return CreatedAtAction(nameof(GetById), new { id = shipDto.Id }, shipDto);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] ShipRequestDto shipDto)
    {
        try
        {
            await shipService.UpdateShipAsync(id, shipDto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await shipService.DeleteShipAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
