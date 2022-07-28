using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CoMute.Web.DAL
{
    public class CoMuteContext: DbContext
    {
        //public CoMuteContext() : base("DefaultConnectionstring")
        //{

        //}

        //public DbSet<Users> users { get; set; }
        public DbSet<CarPool> carPool { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}