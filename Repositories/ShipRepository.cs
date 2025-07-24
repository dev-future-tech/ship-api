using Microsoft.EntityFrameworkCore;
using MySecureWebApi.Data;
using MySecureWebApi.Models;

namespace MySecureWebApi.Repositories;

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
        return await context.Ships.FindAsync(id)?? new Ship();
    }

    public async Task UpdateAsync(Ship ship)
    {
        context.Ships.Update(ship);
        await context.SaveChangesAsync();
    }
}
