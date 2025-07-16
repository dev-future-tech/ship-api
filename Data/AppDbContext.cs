using Microsoft.EntityFrameworkCore;
using Ships.Models;

namespace Ships.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Ship> Ships { get; set; }
    }
}