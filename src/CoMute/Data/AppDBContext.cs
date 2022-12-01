using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using CoMute.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CoMute.Web.Data
{
    public class AppDbContext : IdentityDbContext<IdentityModel>
    {
        public AppDbContext()
            : base("Server=(localdb)\\mssqllocaldb;Database=aspnet-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true", throwIfV1Schema: false)
        {
            
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public DbSet<Carpool> carpoolOpportunities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Carpool>().ToTable("Carpool");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("UserInfo");
        }

    }

    
}