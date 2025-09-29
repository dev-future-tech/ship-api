using Ships.DTOs;

namespace Ships.Services
{
    public interface IOfficerService
    {
        Task<IEnumerable<OfficerResponseDto>> GetAllOfficersAsync();
        Task<OfficerResponseDto> GetOfficerByIdAsync(int id);
        Task AddOfficerAsync(OfficerRequestDto officerRequestDto);
        Task UpdateOfficerAsync(int id, OfficerRequestDto officerRequestDto);
        Task DeleteOfficerAsync(int id);
    }
}