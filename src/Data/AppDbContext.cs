
using FSWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FSWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> context) : base(context)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarpoolMember>()
                .HasKey(x => new { x.CarpoolId, x.UserId });

            modelBuilder.Entity<CarpoolMember>()
                .HasOne<Carpool>()
                .WithMany(s => s.Members)
                .HasForeignKey(x => x.CarpoolId);

            modelBuilder.Entity<CarpoolMember>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<CarpoolMember>()
                .HasKey(x => new { x.CarpoolId, x.UserId });

            modelBuilder.Entity<Carpool>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.OwnerID);

        }

        public DbSet<Carpool> carpools { get; set; }
        public DbSet<User> users { get;set; }
        public DbSet<CarpoolMember> carpoolMembers { get; set; }


    }
}
