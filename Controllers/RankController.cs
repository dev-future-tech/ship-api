using Microsoft.AspNetCore.Mvc;
using MySecureWebApi.DTOs;
using MySecureWebApi.Services;

namespace MySecureWebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RankController(IRankService rankService): ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var ranks = await rankService.GetAllRanksAsync();
            return Ok(ranks);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var rank = await rankService.GetRankByIdAsync(id);
            return Ok(rank);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> AddRank([FromBody] RankRequestDto rankRequestDto)
    {
        await rankService.AddRankAsync(rankRequestDto);
        return CreatedAtAction(nameof(GetById), new {id = rankRequestDto.Id }, rankRequestDto);
    }

    [HttpPut("id")]
    public async Task<IActionResult> UpdateRank(int id, [FromBody] RankRequestDto rankRequestDto)
    {
        try
        {
            await rankService.UpdateRankAsync(id, rankRequestDto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    public async Task<IActionResult> DeleteRank(int id)
    {
        try
        {
            await rankService.DeleteRankAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}