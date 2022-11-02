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
        public DbSet<CarPool> CarPools { get; set; }
        public DbSet<AvailableDay> AvailableDays { get; set; }
        public DbSet<CarPoolMembership> CarPoolMemberships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Configure primary key
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<CarPool>().HasKey(c => c.Id);
            modelBuilder.Entity<AvailableDay>().HasKey(d => d.Id);
            modelBuilder.Entity<CarPoolMembership>().HasKey(m => m.Id);

            //Configure required columns
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Surname).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.EmailAddress).IsRequired();
            modelBuilder.Entity<CarPool>().Property(c => c.DepartureTime).IsRequired();
            modelBuilder.Entity<CarPool>().Property(c => c.ExpectedArrivalTime).IsRequired();
            modelBuilder.Entity<CarPool>().Property(c => c.Origin).IsRequired();
            modelBuilder.Entity<CarPool>().Property(c => c.Destination).IsRequired();
            modelBuilder.Entity<CarPool>().Property(c => c.MaximumSeats).IsRequired();
            modelBuilder.Entity<CarPool>().Property(c => c.AvailableSeats).IsRequired();
            modelBuilder.Entity<CarPool>().Property(c => c.CreatedDate).IsRequired();
            modelBuilder.Entity<AvailableDay>().Property(d => d.Day).IsRequired();
            modelBuilder.Entity<CarPoolMembership>().Property(m => m.CarPoolId).IsRequired();
            modelBuilder.Entity<CarPoolMembership>().Property(m => m.DateJoined).IsRequired();

            //Configure optional columns
            modelBuilder.Entity<User>().Property(u => u.PhoneNumber).IsOptional();
            modelBuilder.Entity<CarPool>().Property(c => c.Notes).IsOptional();

            //Configure relationship
            modelBuilder.Entity<CarPool>().HasRequired<User>(u => u.User)
                .WithMany(c => c.CarPools)
                .HasForeignKey<int>(c => c.UserId);

            modelBuilder.Entity<AvailableDay>().HasRequired<CarPool>(c => c.CarPool)
                .WithMany(d => d.AvailableDays)
                .HasForeignKey<int>(d => d.CarPoolId);

            modelBuilder.Entity<CarPoolMembership>().HasRequired<User>(u => u.User)
                .WithMany(m => m.CarPoolMemberships)
                .HasForeignKey<int>(m => m.UserId);

        }
    }
}