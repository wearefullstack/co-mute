using CoMute.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CoMute.Web.Data
{
    public class ComuteContext : DbContext
    {
        public ComuteContext() : base("ComuteContext")
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Configure primary key
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            //Configure required columns
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Surname).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.EmailAddress).IsRequired();

            //Configure optional columns
            modelBuilder.Entity<User>().Property(u => u.PhoneNumber).IsOptional();
        }
    }
}