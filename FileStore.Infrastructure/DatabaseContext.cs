using FileStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<User> IdentityUsers { get; set; }
    public DbSet<Files> Tests { get; set; }
}