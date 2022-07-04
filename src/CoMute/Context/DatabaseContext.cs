using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

//namespace BookStore.Models
namespace Mutestore.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=CarPoolConnectionString")
        {
            Database.SetInitializer<DatabaseContext>(null);
        }
        public DbSet<RegistrationRequest> RegistrationRequests { get; set; }
        public DbSet<CarPool> CarPools { get; set; }
    }
}
