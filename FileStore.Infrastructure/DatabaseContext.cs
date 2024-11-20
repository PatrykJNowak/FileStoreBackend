using FileStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using File = FileStore.Domain.Entity.File;

namespace FileStore.Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<User> IdentityUsers { get; set; }
    public DbSet<File> File { get; set; }
}