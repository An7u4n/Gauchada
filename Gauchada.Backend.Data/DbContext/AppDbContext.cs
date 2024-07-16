using Microsoft.EntityFrameworkCore;
using Gauchada.Backend.Model.Entity;

namespace Gauchada.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PassengerEntity> Passengers { get; set; }
        public DbSet<DriverEntity> Drivers { get; set; }
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<TripEntity> Trips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PassengerEntity>().ToTable("Passenger");
            modelBuilder.Entity<DriverEntity>().ToTable("Drivers");
            modelBuilder.Entity<CarEntity>().ToTable("Cars");
            modelBuilder.Entity<TripEntity>().ToTable("Trips");

            modelBuilder.Entity<PassengerEntity>()
                .HasMany(p => p.Trips)
                .WithMany(t => t.Passengers)
                .UsingEntity(j => j.ToTable("PassengerTrips"));

            modelBuilder.Entity<DriverEntity>()
                .HasMany(d => d.Trips)
                .WithOne(t => t.Driver)
                .HasForeignKey(t => t.DriverUserName);

            modelBuilder.Entity<DriverEntity>()
                .HasMany(d => d.Cars)
                .WithOne(c => c.Owner)
                .HasForeignKey(c => c.OwnerUserName);

            base.OnModelCreating(modelBuilder);
        }
    }
}

