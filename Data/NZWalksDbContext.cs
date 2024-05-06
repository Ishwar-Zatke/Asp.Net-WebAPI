using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Region>().HasData(new Region() {
                Id = Guid.Parse("21f6b252-7f56-44b7-90ae-a8a92e2e3107"),
                Code = "AKL",
                Name = "Auckland",
                RegionImageUrl = "region-image-url"
            });
            modelBuilder.Entity<Region>().HasData(new Region()
            {
                Id = Guid.Parse("72d9505e-9442-4255-be1a-36f6d012bce2"),
                Code = "DND",
                Name = "Dunedin",
                RegionImageUrl = "region-image-url"
            });
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("a65165d3-293e-4756-be70-10bd10304099"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("25a40027-f82d-4f0a-af0e-7981826379cd"),
                    Name = "Medium"
                },
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

        }
    }
}
