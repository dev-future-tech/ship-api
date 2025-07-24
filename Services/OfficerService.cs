using MySecureWebApi.DTOs;
using MySecureWebApi.Models;
using MySecureWebApi.Repositories;

namespace MySecureWebApi.Services;

public class OfficerService(IOfficerRepository officerRepository) : IOfficerService
{
    public async Task AddOfficerAsync(OfficerRequestDto officerRequestDto)
    {
        var officer = new Officer(officerRequestDto.OfficerName)
        {
            Rank = officerRequestDto.OfficerRank
        };
        await officerRepository.AddAsync(officer);
    }

    public async Task DeleteOfficerAsync(int id)
    {
        var officer = officerRepository.GetByIdAsync(id);

        if (officer == null)
            throw new KeyNotFoundException("Officer not found");

        await officerRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<OfficerResponseDto>> GetAllOfficersAsync()
    {
        var officers = await officerRepository.GetAllAsync();

        return officers.Select(p => new OfficerResponseDto
        {
            OfficerId = p.OfficerId,
            OfficerName = p.OfficerName,
            OfficerRank = p.Rank
        });
    }

    public async Task<OfficerResponseDto> GetOfficerByIdAsync(int id)
    {
        var officer = await officerRepository.GetByIdAsync(id);

        if (officer == null)
            throw new KeyNotFoundException("Officer not found");
        return new OfficerResponseDto
        {
            OfficerId = officer.OfficerId,
            OfficerName = officer.OfficerName,
            OfficerRank = officer.Rank
        };
    }

    public async Task UpdateOfficerAsync(int id, OfficerRequestDto officerRequestDto)
    {
        var officer = await officerRepository.GetByIdAsync(id);

        if (officer == null)
            throw new KeyNotFoundException("Officer not found");

        officer.OfficerName = officerRequestDto.OfficerName;
        officer.Rank = officerRequestDto.OfficerRank;

        await officerRepository.UpdateAsync(officer);
    }
}
