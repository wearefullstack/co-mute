using comute.Models;
using Microsoft.EntityFrameworkCore;

namespace comute.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) :base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<CarPool> CarPools { get; set; }
    public DbSet<JoinCarPool> JoinCarPools { get; set; }
}
