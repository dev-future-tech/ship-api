using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySecureWebApi.DTOs;
using MySecureWebApi.Services;

namespace MySecureWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfficerController(IOfficerService officerService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var officers = await officerService.GetAllOfficersAsync();
        return Ok(officers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var officer = await officerService.GetOfficerByIdAsync(id);
            return Ok(officer);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(OfficerRequestDto officerRequestDto)
    {
        var officerId = await officerService.AddOfficerAsync(officerRequestDto);
        officerRequestDto.Id = officerId;
        return CreatedAtAction(nameof(GetById), new { id = officerRequestDto.Id }, officerRequestDto);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, OfficerRequestDto officerRequestDto)
    {
        try
        {
            await officerService.UpdateOfficerAsync(id, officerRequestDto);
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
            await officerService.DeleteOfficerAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}