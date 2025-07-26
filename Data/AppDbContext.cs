using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MySecureWebApi.Models;

namespace MySecureWebApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ship>()
                .Property(e => e.ShipId)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Rank>()
                .Property(e => e.RankId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Officer>()
                .Property(e => e.OfficerId)
                .ValueGeneratedOnAdd();

            
            modelBuilder.Entity<Officer>()
                .HasOne(o => o.OfficerRank)
                .WithMany(p => p.Officers)
                .HasForeignKey(p => p.OfficerRankId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseNpgsql()
            .UseSeeding((context, _) =>
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
                if(ship5 == null)
                {
                    context.Set<Ship>().Add(new Ship { ShipName = "USS Enterprise", Registry = "NCC-1701-D" });
                }

                // Ranks
                var captainRank = context.Set<Rank>().FirstOrDefault(c => c.RankName == "Captain") ?? context.Set<Rank>().Add(new Rank("Captain")).Entity;


                var commander = context.Set<Rank>().FirstOrDefault(r => r.RankName == "Commander");
                if(commander == null)
                {
                    context.Set<Rank>().Add(new Rank("Commander"));
                }
                
                var lieutenant = context.Set<Rank>().FirstOrDefault(c => c.RankName == "Lieutenant");
                if (lieutenant == null)
                {
                    context.Set<Rank>().Add(new Rank("Lieutenant"));
                }

                var ensign = context.Set<Rank>().FirstOrDefault(c => c.RankName == "Ensign");
                if (ensign == null)
                {
                    context.Set<Rank>().Add(new Rank("Ensign"));
                }

                context.SaveChanges();
                

                // Officers
                var officer1 = context.Set<Officer>().FirstOrDefault(c => c.OfficerName == "Katherine Janeway");
                if (officer1 == null)
                {
                    context.Set<Officer>().Add(new Officer
                    {
                        OfficerName = "Katherine Janeway",
                        OfficerRank = captainRank,
                        OfficerRankId = captainRank.RankId,
                    });
                }

                var officer2 = context.Set<Officer>().FirstOrDefault(c => c.OfficerName == "Jean-Luc Picard");
                if (officer2 == null)
                {
                    context.Set<Officer>().Add(new Officer
                    {
                        OfficerName = "Jean-Luc Picard",
                        OfficerRankId = captainRank.RankId,
                        OfficerRank = captainRank
                    });
                }

                var officer3 = context.Set<Officer>().FirstOrDefault(c => c.OfficerName == "Benjamin Sisko");
                if (officer3 == null)
                {
                    context.Set<Officer>().Add(new Officer
                    {
                        OfficerName = "Benjamin Sisko",
                        OfficerRankId = captainRank.RankId,
                        OfficerRank = captainRank
                    });
                }

                var officer4 = context.Set<Officer>().FirstOrDefault(c => c.OfficerName == "Michael Burnham");
                if (officer4 == null)
                {
                    context.Set<Officer>().Add(new Officer
                    {
                        OfficerName = "Michael Burnham",
                        OfficerRankId = captainRank.RankId,
                        OfficerRank = captainRank
                    });
                }

                var officer5 = context.Set<Officer>().FirstOrDefault(c => c.OfficerName == "Christopher Pike");
                if (officer5 == null)
                {
                    context.Set<Officer>().Add(new Officer
                    {
                        OfficerName = "Christopher Pike",
                        OfficerRankId = captainRank.RankId,
                        OfficerRank = captainRank
                    });
                }

                context.SaveChanges();

            });
        }

        public DbSet<Ship> Ships { get; set; }

        public DbSet<Rank> Ranks { get; set; }
        
        public DbSet<Officer> Officers { get; set; }
        
    }
}