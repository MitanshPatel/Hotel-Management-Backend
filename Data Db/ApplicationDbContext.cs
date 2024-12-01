using Microsoft.EntityFrameworkCore;
using Hostel_Management.Models;

namespace Hostel_Management.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public required DbSet<User> Users { get; set; }
        public required DbSet<Room> Rooms { get; set; }
        public required DbSet<Reservation> Reservations { get; set; }
        public required DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).ValueGeneratedOnAdd();
                entity.Property(e => e.Username).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Role).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.IsActive).IsRequired();
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Rooms");
                entity.HasKey(e => e.RoomId);
                entity.Property(e => e.RoomId).ValueGeneratedOnAdd();
                entity.Property(e => e.RoomType).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.BedType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.View).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservations");
                entity.HasKey(e => e.ReservationId);
                entity.Property(e => e.ReservationId).ValueGeneratedOnAdd();
                entity.Property(e => e.GuestId).IsRequired();
                entity.Property(e => e.RoomId).IsRequired();
                entity.Property(e => e.CheckInDate).IsRequired();
                entity.Property(e => e.CheckOutDate).IsRequired();
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PaymentStatus).IsRequired().HasMaxLength(50);
                entity.Property(e => e.SpecialRequests).HasMaxLength(500);

                // Define foreign key relationships
                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(e => e.GuestId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Room>()
                      .WithMany()
                      .HasForeignKey(e => e.RoomId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");
                entity.HasKey(e => e.PaymentId);
                entity.Property(e => e.PaymentId).ValueGeneratedOnAdd();
                entity.Property(e => e.ReservationId).IsRequired();
                entity.Property(e => e.Amount).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.PaymentMethod).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PaymentFor).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PaymentDate).IsRequired();
                entity.Property(e => e.Status).IsRequired().HasMaxLength(50);

                // Define foreign key relationship
                entity.HasOne<Reservation>()
                      .WithMany()
                      .HasForeignKey(e => e.ReservationId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
