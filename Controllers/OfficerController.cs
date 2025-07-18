using Microsoft.AspNetCore.Mvc;
using Ships.DTOs;
using Ships.Services;

namespace Ships.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfficerController : ControllerBase
    {
        private readonly IOfficerService _officerService;

        public OfficerController(IOfficerService officerService)
        {
            _officerService = officerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var officers = await _officerService.GetAllOfficersAsync();
            return Ok(officers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var officer = await _officerService.GetOfficerByIdAsync(id);
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
            await _officerService.AddOfficerAsync(officerRequestDto);
            return CreatedAtAction(nameof(GetById), new { id = officerRequestDto.Id }, officerRequestDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, OfficerRequestDto officerRequestDto)
        {
            try
            {
                await _officerService.UpdateOfficerAsync(id, officerRequestDto);
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
                await _officerService.DeleteOfficerAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}