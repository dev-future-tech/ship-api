using Microsoft.EntityFrameworkCore;
using Ships.Models;
using Ships.Data;

namespace Ships.Repositories
{
    public class OfficerRepository : IOfficerRepository
    {
        private readonly AppDbContext _context;

        public OfficerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Officer officer)
        {
            await _context.Officers.AddAsync(officer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int officerId)
        {
            var officer = await _context.Officers.FindAsync(officerId);
            if (officer != null)
            {
                _context.Officers.Remove(officer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Officer>> GetAllAsync()
        {
            return await _context.Officers.ToListAsync();
        }

        public async Task<Officer> GetByIdAsync(int id)
        {
            return await _context.Officers.FindAsync(id);
        }

        public async Task UpdateAsync(Officer officer)
        {
            _context.Officers.Update(officer);
            await _context.SaveChangesAsync();
        }
    }


}