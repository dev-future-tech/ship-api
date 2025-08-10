using MySecureWebApi.Models;

namespace MySecureWebApi.Repositories;

public interface IOfficerRepository
{
    Task<IEnumerable<Officer>> GetAllAsync();
    Task<Officer> GetByIdAsync(int id);
    Task<IEnumerable<Officer>> GetOfficersByQueryAsync(string rankName);
    Task<int> AddAsync(Officer officer);
    Task UpdateAsync(Officer officer);
    Task DeleteAsync(int officerId);
}
