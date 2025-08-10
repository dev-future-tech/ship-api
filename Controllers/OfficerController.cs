using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySecureWebApi.DTOs;
using MySecureWebApi.Services;

namespace MySecureWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfficerController(IOfficerService officerService) : ControllerBase
{
    // [HttpGet]
    // [Authorize]
    // public async Task<IActionResult> GetAll()
    // {
    //     var officers = await officerService.GetAllOfficersAsync();
    //     return Ok(officers);
    // }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByQuery([FromQuery] OfficerQuery query)
    {
        if (query.rankName == null)
        {
            var officers = await officerService.GetAllOfficersAsync();
            return Ok(officers);
        }
        else
        {
            var officers = await officerService.GetAllOfficersWithRankAsync(query!.rankName);
            return Ok(officers);
        }
    }

    [HttpGet("{id}")]
    [Authorize("read:officers")]
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
    [Authorize]
    public async Task<IActionResult> Add(OfficerRequestDto officerRequestDto)
    {
        var officerId = await officerService.AddOfficerAsync(officerRequestDto);
        officerRequestDto.Id = officerId;
        return CreatedAtAction(nameof(GetById), new { id = officerRequestDto.Id }, officerRequestDto);
    }

    [HttpPut]
    [Authorize]
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
    [Authorize]
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