using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Ships.Models;

namespace Ships.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseNpgsql()
            .UseSeeding( (context, _) =>
            {
                // Ships
                var ship1 = context.Set<Ship>().FirstOrDefault(b => b.Registry == "NCC-74656");
                if (ship1 == null)
                {
                    context.Set<Ship>().Add(new Ship { ShipName = "USS Voyager", Registry = "NCC-74656" });
                }

                var ship2 = context.Set<Ship>().FirstOrDefault(b => b.Registry == "NCC-1031-A");
                if (ship2 == null)
                {
                    context.Set<Ship>().Add(new Ship { ShipName = "USS Discovery", Registry = "NCC-1031-A" });
                }

                var ship3 = context.Set<Ship>().FirstOrDefault(b => b.Registry == "NX-74205");
                if (ship3 == null)
                {
                    context.Set<Ship>().Add(new Ship { ShipName = "USS Defiant", Registry = "NX-74205" });
                }

                var ship4 = context.Set<Ship>().FirstOrDefault(b => b.Registry == "NCC-1701");
                if (ship4 == null)
                {
                    context.Set<Ship>().Add(new Ship { ShipName = "USS Enterprise", Registry = "NCC-1701" });
                }

                var ship5 = context.Set<Ship>().FirstOrDefault(b => b.Registry == "NCC-1701-D");
                {
                    context.Set<Ship>().Add(new Ship { ShipName = "USS Enterprise", Registry = "NCC-1701-D" });
                }

                // Officers
                var officer1 = context.Set<Officer>().FirstOrDefault(c => c.OfficerName == "Katherine Janeway");
                if (officer1 == null)
                {
                    context.Set<Officer>().Add(new Officer { OfficerName = "Katherine Janeway", Rank = "Captain" });
                }

                var officer2 = context.Set<Officer>().FirstOrDefault(c => c.OfficerName == "Jean-Luc Picard");
                if (officer2 == null)
                {
                    context.Set<Officer>().Add(new Officer { OfficerName = "Jean-Luc Picard", Rank = "Captain" });
                }

                var officer3 = context.Set<Officer>().FirstOrDefault(c => c.OfficerName == "Benjamin Sisko");
                if (officer3 == null)
                {
                    context.Set<Officer>().Add(new Officer { OfficerName = "Benjamin Sisko", Rank = "Captain" });
                }

                var officer4 = context.Set<Officer>().FirstOrDefault(c => c.OfficerName == "Michael Burnham");
                if (officer4 == null)
                {
                    context.Set<Officer>().Add(new Officer { OfficerName = "Michael Burnham", Rank = "Captain" });
                }

                var officer5 = context.Set<Officer>().FirstOrDefault(c => c.OfficerName == "Christopher Pike");
                if (officer5 == null)
                {
                    context.Set<Officer>().Add(new Officer { OfficerName = "Christopher Pike", Rank = "Captain" });
                }


                context.SaveChanges();

            })
            .UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
            });
        }

        public DbSet<Ship> Ships { get; set; }

        public DbSet<Officer> Officers { get; set; }
    }
}