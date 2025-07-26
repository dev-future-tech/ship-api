using Microsoft.EntityFrameworkCore;
using MySecureWebApi.Data;
using MySecureWebApi.Models;

namespace MySecureWebApi.Repositories;

public class OfficerRepository(AppDbContext context) : IOfficerRepository
{
    public async Task AddAsync(Officer officer)
    {
        await context.Officers.AddAsync(officer);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int officerId)
    {
        var officer = await context.Officers.FindAsync(officerId);
        if (officer != null)
        {
            context.Officers.Remove(officer);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Officer>> GetAllAsync()
    {
        return await context.Officers.ToListAsync();
    }

    public async Task<Officer> GetByIdAsync(int id)
    {
        var officer = await context.Officers.FindAsync(id);
        if (officer == null)
            throw new KeyNotFoundException($"Officer not found: {id}");

        return officer;
    }

    public async Task UpdateAsync(Officer officer)
    {
        context.Officers.Update(officer);
        await context.SaveChangesAsync();
    }
}