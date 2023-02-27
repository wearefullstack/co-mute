using CoMute.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoMute.Web.Context
{
    public class CoMuteDbContext : DbContext
    {
        //public CoMuteDbContext(DbContextOptions<CoMuteDbContext> options)
        //   : base(options)
        //{

        //}

        public CoMuteDbContext() : base("CoMuteDbContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CarPool> CarPools { get; set; }
        public DbSet<CarPoolDays> CarPoolDays { get; set; }
        public DbSet<CarPoolMembers> CarPoolMembers { get; set; }
    }
}