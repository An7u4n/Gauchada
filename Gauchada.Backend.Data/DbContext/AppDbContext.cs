using Microsoft.EntityFrameworkCore;
using Gauchada.Backend.Model.Entity;

namespace Gauchada.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PassengerEntity> Passengers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PassengerEntity>().ToTable("Passenger");
            modelBuilder.Entity<PassengerEntity>().HasKey(p => p.UserName);
            base.OnModelCreating(modelBuilder);
        }
    }
}

