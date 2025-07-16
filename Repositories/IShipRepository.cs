using Ships.Models;

namespace Ships.Repositories
{
    public interface IShipRepository
    {
        Task<IEnumerable<Ship>> GetAllAsync();
        Task<Ship> GetByIdAsync(int id);
        Task AddAsync(Ship ship);
        Task UpdateAsync(Ship ship);
        Task DeleteAsync(int shipId);
    }
}