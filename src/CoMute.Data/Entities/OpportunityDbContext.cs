using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Data.Entities
{
    public class OpportunityDbContext:IdentityDbContext<User>
    {
        public OpportunityDbContext(DbContextOptions<OpportunityDbContext> options) : base(options)
        {
        }
        public DbSet<CarPoolOpportunity> CarPoolOpportunity { get; set; }
        public DbSet<UserOpportunity> UserOpportunity { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<CarPoolOpportunity>()
            //.HasOne<User>()
            //.WithMany()
            //.HasForeignKey(o => o.UserId)
            //.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserOpportunity>()
                  .HasKey(uo => new { uo.Id });

            modelBuilder.Entity<UserOpportunity>()
                .HasOne(uo => uo.User)
                .WithMany(u => u.UserOpportunities)
                .HasForeignKey(uo => uo.UserId);

            modelBuilder.Entity<UserOpportunity>()
                .HasOne(uo => uo.CarPoolOpportunity)
                .WithMany(o => o.UserOpportunities)
                .HasForeignKey(uo => uo.OpportunityId);
        }
    }
}
