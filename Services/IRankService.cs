using MySecureWebApi.DTOs;

namespace MySecureWebApi.Services;

public interface IRankService
{
    Task<IEnumerable<RankResponseDto>> GetAllRanksAsync();
    Task<RankResponseDto> GetRankByIdAsync(int id);
    Task AddRankAsync(RankRequestDto rankRequestDto);
    Task UpdateRankAsync(int id, RankRequestDto rankRequestDto);
    Task DeleteRankAsync(int id);
}