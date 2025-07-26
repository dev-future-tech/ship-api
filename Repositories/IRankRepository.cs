using MySecureWebApi.Models;

namespace MySecureWebApi.Repositories;

public interface IRankRepository
{
    Task<IEnumerable<Rank>> GetRanksAllAsync();
    Task<Rank> GetRankByIdAsync(int id);
    Task<Rank> GetRankByNameAsync(string name);
    Task AddRankAsync(Rank rank);
    Task UpdateRankAsync(Rank rank);
    Task DeleteRankAsync(int rankId);
}
