using Microsoft.EntityFrameworkCore;
using CoMute.Models;

namespace CoMute
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<CarPool> CarPools { get; set; }
        public DbSet<UserCarPool> UserCarPools { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}