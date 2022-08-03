using CoMute.Core.Domain;
using CoMute.DB.Mappings;
using System.Data.Entity;

namespace CoMute.DB
{
    public interface ICoMuteDbContext
    {
        IDbSet<User> Users { get; set; }
        int SaveChanges();
    }
    public class CoMuteDbContext : DbContext, ICoMuteDbContext
    {
        static CoMuteDbContext()
        {
            Database.SetInitializer<CoMuteDbContext>(null);
        }

        public CoMuteDbContext(string nameOrConnectionString = null)
            : base(nameOrConnectionString ?? "Name=CoMuteWebContext")
        {            
        }

        public IDbSet<User> Users { get; set; }  

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            var config = modelBuilder.Configurations;
            config.Add(new UserMap());
        }
    }
}