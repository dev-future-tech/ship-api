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
                .WithMany()
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

            })
            .UseAsyncSeeding(async (context, _, cancellationToken) =>
            { 
                // Ships
                var ship1 = await context.Set<Ship>().FirstOrDefaultAsync(b => b.Registry == "NCC-74656", cancellationToken);
                if (ship1 == null)
                {
                    await context.Set<Ship>().AddAsync(new Ship { ShipName = "USS Voyager", Registry = "NCC-74656" }, cancellationToken);
                }

                var ship2 = await context.Set<Ship>().FirstOrDefaultAsync(b => b.Registry == "NCC-1031-A", cancellationToken);
                if (ship2 == null)
                {
                    await context.Set<Ship>().AddAsync(new Ship { ShipName = "USS Discovery", Registry = "NCC-1031-A" }, cancellationToken);
                }

                var ship3 = await context.Set<Ship>().FirstOrDefaultAsync(b => b.Registry == "NX-74205", cancellationToken);
                if (ship3 == null)
                {
                    await context.Set<Ship>().AddAsync(new Ship { ShipName = "USS Defiant", Registry = "NX-74205" }, cancellationToken);
                }

                var ship4 = await context.Set<Ship>().FirstOrDefaultAsync(b => b.Registry == "NCC-1701", cancellationToken);
                if (ship4 == null)
                {
                    await context.Set<Ship>().AddAsync(new Ship { ShipName = "USS Enterprise", Registry = "NCC-1701" }, cancellationToken);
                }

                var ship5 = await context.Set<Ship>().FirstOrDefaultAsync(b => b.Registry == "NCC-1701-D", cancellationToken);
                if(ship5 == null)
                {
                    await context.Set<Ship>().AddAsync(new Ship { ShipName = "USS Enterprise", Registry = "NCC-1701-D" }, cancellationToken);
                }

                // Ranks
                var captainRank = await context.Set<Rank>()
                    .FirstOrDefaultAsync(c => c.RankName == "Captain", cancellationToken);
                if (captainRank == null)
                {
                    await context.Set<Rank>().AddAsync(new Rank("Captain"), cancellationToken);
                    captainRank = await context.Set<Rank>().FirstAsync(c => c.RankName == "Captain", cancellationToken);
                }


                var commander = await context.Set<Rank>().FirstOrDefaultAsync(r => r.RankName == "Commander", cancellationToken);
                if(commander == null)
                {
                    await context.Set<Rank>().AddAsync(new Rank("Commander"), cancellationToken);
                }
                
                var lieutenant = await context.Set<Rank>().FirstOrDefaultAsync(c => c.RankName == "Lieutenant", cancellationToken);
                if (lieutenant == null)
                {
                    await context.Set<Rank>().AddAsync(new Rank("Lieutenant"), cancellationToken);
                }

                var ensign = await context.Set<Rank>().FirstOrDefaultAsync(c => c.RankName == "Ensign", cancellationToken);
                if (ensign == null)
                {
                    await context.Set<Rank>().AddAsync(new Rank("Ensign"), cancellationToken);
                }

                await context.SaveChangesAsync(cancellationToken);
                

                // Officers
                var officer1 = await context.Set<Officer>().FirstOrDefaultAsync(c => c.OfficerName == "Katherine Janeway", cancellationToken);
                if (officer1 == null)
                {
                    await context.Set<Officer>().AddAsync(new Officer
                    {
                        OfficerName = "Katherine Janeway",
                        OfficerRank = captainRank,
                        OfficerRankId = captainRank.RankId,
                    }, cancellationToken);
                }

                var officer2 = await context.Set<Officer>().FirstOrDefaultAsync(c => c.OfficerName == "Jean-Luc Picard", cancellationToken);
                if (officer2 == null)
                {
                    await context.Set<Officer>().AddAsync(new Officer
                    {
                        OfficerName = "Jean-Luc Picard",
                        OfficerRankId = captainRank.RankId,
                        OfficerRank = captainRank
                    }, cancellationToken);
                }

                var officer3 = await context.Set<Officer>().FirstOrDefaultAsync(c => c.OfficerName == "Benjamin Sisko", cancellationToken);
                if (officer3 == null)
                {
                    await context.Set<Officer>().AddAsync(new Officer
                    {
                        OfficerName = "Benjamin Sisko",
                        OfficerRankId = captainRank.RankId,
                        OfficerRank = captainRank
                    }, cancellationToken);
                }

                var officer4 = await context.Set<Officer>().FirstOrDefaultAsync(c => c.OfficerName == "Michael Burnham", cancellationToken);
                if (officer4 == null)
                {
                    await context.Set<Officer>().AddAsync(new Officer
                    {
                        OfficerName = "Michael Burnham",
                        OfficerRankId = captainRank.RankId,
                        OfficerRank = captainRank
                    }, cancellationToken);
                }

                var officer5 = await context.Set<Officer>().FirstOrDefaultAsync(c => c.OfficerName == "Christopher Pike", cancellationToken);
                if (officer5 == null)
                {
                    await context.Set<Officer>().AddAsync(new Officer
                    {
                        OfficerName = "Christopher Pike",
                        OfficerRankId = captainRank.RankId,
                        OfficerRank = captainRank
                    }, cancellationToken);
                }

                await context.SaveChangesAsync(cancellationToken);

            });
        }

        public DbSet<Ship> Ships { get; set; }

        public DbSet<Rank> Ranks { get; set; }
        
        public DbSet<Officer> Officers { get; set; }
        
    }
}