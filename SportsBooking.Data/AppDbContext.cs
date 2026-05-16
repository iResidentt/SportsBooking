using Microsoft.EntityFrameworkCore;
using SportsBooking.Models;

namespace SportsBooking.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<SportsGround> SportsGrounds => Set<SportsGround>();

    public DbSet<GroundType> GroundTypes => Set<GroundType>();

    public DbSet<Booking> Bookings => Set<Booking>();

    public DbSet<BookingStatus> BookingStatuses => Set<BookingStatus>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<SportsGround>()
    .HasOne(g => g.GroundType)
    .WithMany(t => t.SportsGrounds)
    .HasForeignKey(g => g.GroundTypeId)
    .IsRequired(false);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.SportsGround)
            .WithMany(g => g.Bookings)
            .HasForeignKey(b => b.SportsGroundId);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Status)
            .WithMany(s => s.Bookings)
            .HasForeignKey(b => b.StatusId);
    }
}
