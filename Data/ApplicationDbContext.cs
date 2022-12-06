using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Co_Mute.Models;


namespace Co_Mute.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

       
        public DbSet<Assignments> Assignments { get; set; }

             
            
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

        }

       
    }
}