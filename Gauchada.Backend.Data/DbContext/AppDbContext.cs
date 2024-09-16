using Microsoft.EntityFrameworkCore;
using Gauchada.Backend.Model.Entity;
using Gauchada.Backend.Model.Entity.Abstract;

namespace Gauchada.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PassengerEntity> Passengers { get; set; }
        public DbSet<DriverEntity> Drivers { get; set; }
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<TripEntity> Trips { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<DriverMessage> DriverMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PassengerEntity>().ToTable("Passenger");
            modelBuilder.Entity<DriverEntity>().ToTable("Drivers");
            modelBuilder.Entity<CarEntity>().ToTable("Cars");
            modelBuilder.Entity<TripEntity>().ToTable("Trips");
            modelBuilder.Entity<Chat>().ToTable("Chats");
            modelBuilder.Entity<Message>().ToTable("Messages");
            modelBuilder.Entity<DriverMessage>().ToTable("DriverMessages");

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

            modelBuilder.Entity<CarEntity>()
                .HasMany(c => c.Trips)
                .WithOne(t => t.Car)
                .HasForeignKey(t => t.CarPlate);

            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId);

            modelBuilder.Entity<PassengerEntity>()
                .HasMany(u => u.Messages)
                .WithOne(m => m.Writer)
                .HasForeignKey(m => m.WriterUsername)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DriverEntity>()
               .HasMany(u => u.Messages)
               .WithOne(m => m.Writer)
               .HasForeignKey(m => m.WriterUsername)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TripEntity>()
                .HasOne(t => t.Chat)
                .WithOne(c => c.Trip)
                .HasForeignKey<Chat>(c => c.TripId)
                .OnDelete(DeleteBehavior.NoAction);



            base.OnModelCreating(modelBuilder);
        }
    }
}

