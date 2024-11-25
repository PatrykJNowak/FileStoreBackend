using FileStore.Domain.IdentityEntity;
using Microsoft.EntityFrameworkCore;
using File = FileStore.Domain.Entities.File;

namespace FileStore.Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<User> IdentityUsers { get; set; }
    public DbSet<File> File { get; set; }
}