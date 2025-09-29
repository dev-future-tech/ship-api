using Microsoft.EntityFrameworkCore;
using Ships.Models;
using Ships.Data;

namespace Ships.Repositories
{
    public class ShipRepository : IShipRepository
    {
        private readonly AppDbContext _context;

        public ShipRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Ship ship)
        {
            await _context.Ships.AddAsync(ship);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int shipId)
        {
            var ship = await _context.Ships.FindAsync(shipId);
            if (ship != null)
            {
                _context.Ships.Remove(ship);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Ship>> GetAllAsync()
        {
            return await _context.Ships.ToListAsync();
        }

        public async Task<Ship> GetByIdAsync(int id)
        {
            return await _context.Ships.FindAsync(id);
        }

        public async Task UpdateAsync(Ship ship)
        {
            _context.Ships.Update(ship);

            await _context.SaveChangesAsync();
        }
    }
}