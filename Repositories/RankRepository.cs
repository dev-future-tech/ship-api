using Microsoft.EntityFrameworkCore;
using MySecureWebApi.Data;
using MySecureWebApi.Models;

namespace MySecureWebApi.Repositories;

public class RankRepository(AppDbContext context) : IRankRepository
{
    public async Task<IEnumerable<Rank>> GetRanksAllAsync()
    {
        return await context.Ranks.ToListAsync();
    }

    public async Task<Rank> GetRankByIdAsync(int id)
    {
        return await context.Ranks.FindAsync(id)?? new Rank("Not found");;
    }

    public async Task AddRankAsync(Rank rank)
    {
        await context.Ranks.AddAsync(rank);
        await context.SaveChangesAsync();
    }

    public async Task UpdateRankAsync(Rank rank)
    {
        context.Ranks.Update(rank);
        await context.SaveChangesAsync();
    }

    public async Task DeleteRankAsync(int rankId)
    {
        var rank = await context.Ranks.FindAsync(rankId);
        if (rank != null)
        {
            context.Ranks.Remove(rank);
        }
    }
}
