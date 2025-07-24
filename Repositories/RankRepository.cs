using Microsoft.EntityFrameworkCore;
using MySecureWebApi.Data;
using MySecureWebApi.Models;

namespace MySecureWebApi.Repositories;

public class RankRepository(AppDbContext context) : IRankRepository
{
    public async Task<IEnumerable<Rank>> GetAllAsync()
    {
        return await context.Ranks.ToListAsync();
    }

    public async Task<Rank> GetByIdAsync(int id)
    {
        return await context.Ranks.FindAsync(id)?? new Rank();;
    }

    public async Task AddAsync(Rank rank)
    {
        await context.Ranks.AddAsync(rank);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Rank rank)
    {
        context.Ranks.Update(rank);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int rankId)
    {
        var rank = await context.Ranks.FindAsync(rankId);
        if (rank != null)
        {
            context.Ranks.Remove(rank);
        }
    }
}
