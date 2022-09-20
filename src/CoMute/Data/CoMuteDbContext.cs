using CoMute.Web.Data.Entities;
using CoMute.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;

namespace CoMute.Web.Data
{
    public class CoMuteDbContext : IdentityDbContext<User>
    {
        public CoMuteDbContext()
            : base("name=CoMuteDbContext")
        {
        }

         public virtual DbSet<CarPool>  CarPools { get; set; }
    }


}