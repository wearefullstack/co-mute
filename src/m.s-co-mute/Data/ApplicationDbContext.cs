using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace m.s_co_mute.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<m.s_co_mute.Models.CarPool>? CarPool { get; set; }
        public DbSet<m.s_co_mute.Models.User>? User { get; set; }

    }
}