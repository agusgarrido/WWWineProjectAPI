using Microsoft.EntityFrameworkCore;
using WWWineProjectAPI.Models;

namespace WWWineProjectAPI.Data
{
    public class WWWineProjectDb: DbContext
    {

        public WWWineProjectDb(DbContextOptions<WWWineProjectDb> options) : base(options) 
        {
        
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Variety> Varieties { get; set; }
        public DbSet<Flavor> Flavors { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<VarietyFlavor> VarietyFlavors { get; set; }
        public DbSet<VarietyRegion> VarietyRegions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VarietyFlavor>()
                .HasKey(vf => vf.VarietyFlavorID);

            modelBuilder.Entity<VarietyFlavor>()
                .HasOne(vf => vf.Variety)
                .WithMany(v => v.Flavors)
                .HasForeignKey(vf => vf.VarietyID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VarietyFlavor>()
                .HasOne(vf => vf.Flavor)
                .WithMany(f => f.Varieties)
                .HasForeignKey(vf => vf.FlavorID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VarietyRegion>()
                .HasKey(vr => vr.VarietyRegionID);

            modelBuilder.Entity<VarietyRegion>()
                .HasOne(vr => vr.Variety)
                .WithMany(v => v.Regions)
                .HasForeignKey(vr => vr.VarietyID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VarietyRegion>()
                .HasOne(vr => vr.Region)
                .WithMany(r => r.Varieties)
                .HasForeignKey(vr => vr.RegionID)
                .OnDelete(DeleteBehavior.NoAction);

            // EXAMPLE DATA
            modelBuilder.Entity<Country>()
                .HasData(
                new Country { CountryID = 1, Name = "Francia"},
                new Country { CountryID = 2, Name = "Argentina"}
                );

            modelBuilder.Entity<Color>()
                .HasData(
                new Color { ColorID = 1, Name = "Tinta" },
                new Color { ColorID = 2, Name = "Blanca" }
                );

            modelBuilder.Entity<Region>()
                .HasData(
                new Region { RegionID = 1, Name = "Burdeos", CountryID = 1 },
                new Region { RegionID = 2, Name = "Valle del Loire", CountryID = 1 },
                new Region { RegionID = 3, Name = "Mendoza", CountryID = 2}
                );

            modelBuilder.Entity<Variety>()
                .HasData(
                new Variety { VarietyID = 1, Name = "Chenin Blanc", ColorID = 2, OriginID = 1},
                new Variety { VarietyID = 2, Name = "Cabernet Sauvignon", ColorID = 1, OriginID = 1 }
                );

            modelBuilder.Entity<VarietyRegion>()
                .HasData(
                new VarietyRegion { VarietyRegionID = 1, VarietyID = 1, RegionID = 2},
                new VarietyRegion { VarietyRegionID = 2, VarietyID = 1, RegionID = 3},
                new VarietyRegion { VarietyRegionID = 3, VarietyID = 2, RegionID = 1},
                new VarietyRegion { VarietyRegionID = 4, VarietyID = 2, RegionID = 3}
                );
        } 
    }
}
