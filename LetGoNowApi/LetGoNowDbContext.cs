using Microsoft.EntityFrameworkCore;
using LetGoNowApi.Models;

namespace LetGoNowApi
{
    public class LetGoNowDbContext : DbContext
    {
        public LetGoNowDbContext(DbContextOptions<LetGoNowDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Cruise> Cruises { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình ánh xạ StartDate và EndDate sang kiểu date
            modelBuilder.Entity<Booking>()
                .Property(b => b.StartDate)
                .HasColumnType("date");

            modelBuilder.Entity<Booking>()
                .Property(b => b.EndDate)
                .HasColumnType("date");
        }
    }
}