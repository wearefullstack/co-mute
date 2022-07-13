using co_mute_be.Models;
using Microsoft.EntityFrameworkCore;

namespace co_mute_be.Database
{
        public class UserContext : DbContext
        {
            public UserContext(DbContextOptions<UserContext> options)
                : base(options)
            {
            }

            public DbSet<User> CarPoolOpps { get; set; } = null!;
        }
}
