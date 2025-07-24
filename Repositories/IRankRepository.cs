using MySecureWebApi.Models;

namespace MySecureWebApi.Repositories;

public interface IRankRepository
{
    Task<IEnumerable<Rank>> GetAllAsync();
    Task<Rank> GetByIdAsync(int id);
    Task AddAsync(Rank rank);
    Task UpdateAsync(Rank rank);
    Task DeleteAsync(int rankId);
}
