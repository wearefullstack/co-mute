using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoMuteCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CoMuteCore.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        public DbSet<User> User { get; set; }
        public DbSet<CarPool> CarPools { get; set; }
        public DbSet<UserCarPool> UserCarPools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}