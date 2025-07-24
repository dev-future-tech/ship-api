using MySecureWebApi.DTOs;
using MySecureWebApi.Models;
using MySecureWebApi.Repositories;

namespace MySecureWebApi.Services;

public class RankService(IRankRepository rankRepository) : IRankService
{
    public async Task<IEnumerable<RankResponseDto>> GetAllRanksAsync()
    {
        var ranks = await rankRepository.GetRanksAllAsync();
        return ranks.Select(rank => new RankResponseDto(rank.RankId, rank.RankName));
    }

    public async Task<RankResponseDto> GetRankByIdAsync(int id)
    {
        var rank = await rankRepository.GetRankByIdAsync(id);
        
        if(rank == null)
            throw new KeyNotFoundException("Rank not found");
        
        return new RankResponseDto(rank.RankId, rank.RankName);
    }

    public async Task AddRankAsync(RankRequestDto rankRequestDto)
    {
        var rank = new Rank(rankRequestDto.RankName) { };
        await rankRepository.AddRankAsync(rank);
    }

    public async Task UpdateRankAsync(int id, RankRequestDto rankRequestDto)
    {
        var rank = await rankRepository.GetRankByIdAsync(id);
        
        if(rank == null)
            throw new KeyNotFoundException("Rank not found");
        rank.RankName = rankRequestDto.RankName;
        await rankRepository.UpdateRankAsync(rank);
    }

    public async Task DeleteRankAsync(int id)
    {
        var rank = rankRepository.GetRankByIdAsync(id).Result;
        if(rank == null)
            throw new KeyNotFoundException("Rank not found");

        await rankRepository.DeleteRankAsync(id);
    }
}