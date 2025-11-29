using Microsoft.EntityFrameworkCore;
using Ships.Models;
using Ships.Data;

namespace Ships.Repositories;

public class ShipRepository(AppDbContext context) : IShipRepository
{
    public async Task AddAsync(Ship ship)
    {
        await context.Ships.AddAsync(ship);

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int shipId)
    {
        var ship = await context.Ships.FindAsync(shipId);
        if (ship != null)
        {
            context.Ships.Remove(ship);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Ship>> GetAllAsync()
    {
        return await context.Ships.ToListAsync();
    }

    public async Task<Ship> GetByIdAsync(int id)
    {
        var ship = await context.Ships.FindAsync(id);
        if (ship == null)
        {
            throw new KeyNotFoundException("Ship not found");
        }
        return ship;
    }

    public async Task UpdateAsync(Ship ship)
    {
        context.Ships.Update(ship);

        await context.SaveChangesAsync();
    }
}