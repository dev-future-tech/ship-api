using MySecureWebApi.DTOs;
using MySecureWebApi.Models;
using MySecureWebApi.Repositories;

namespace MySecureWebApi.Services;

public class OfficerService(IOfficerRepository officerRepository, IRankRepository rankRepository) : IOfficerService
{
    public async Task<int> AddOfficerAsync(OfficerRequestDto officerRequestDto)
    {
        try
        {
            var rank = await rankRepository.GetRankByNameAsync(officerRequestDto.OfficerRank);
            var officer = new Officer
            {
                OfficerName = officerRequestDto.OfficerName,
                OfficerRankId = rank.RankId,
                OfficerRank = rank
            };
            var savedOfficerId = await officerRepository.AddAsync(officer);
            return savedOfficerId;
            
        }
        catch (KeyNotFoundException ex)
        {
            throw new CreateOfficerException($"Rank not found to tbe allocated: {ex.Message}");
        }
        
        
            
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
            OfficerName = p.OfficerName ?? "",
            OfficerRank = p.OfficerRank.RankName
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
            OfficerName = officer.OfficerName ?? "",
            OfficerRank = officer.OfficerRank.RankName,
        };
    }

    public async Task UpdateOfficerAsync(int id, OfficerRequestDto officerRequestDto)
    {
        var rank = await rankRepository.GetRankByNameAsync(officerRequestDto.OfficerRank);
        if (rank == null)
        {
            throw new UpdateOfficerException($"Rank {officerRequestDto.OfficerRank} not found to tbe allocated");
        }
        
        var officer = await officerRepository.GetByIdAsync(id);

        if (officer == null)
            throw new KeyNotFoundException("Officer not found");

        officer.OfficerName = officerRequestDto.OfficerName;
        officer.OfficerRankId = rank.RankId;
        officer.OfficerRank = rank;

        await officerRepository.UpdateAsync(officer);
    }
}
