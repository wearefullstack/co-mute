using co_mute_be.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace co_mute_be.Database
{
        public class CarPoolOppContext : DbContext
        {
            public CarPoolOppContext(DbContextOptions<CarPoolOppContext> options)
                : base(options)
            {
            }

            public DbSet<CarPoolOpp> CarPoolOpps { get; set; } = null!;
        }
}
