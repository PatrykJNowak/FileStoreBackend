using FileStore.Domain.IdentityEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Infrastructure;

public class IdentityDatabaseContext : IdentityDbContext<User>
{
    public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options)
        : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("identity");
        
        base.OnModelCreating(builder);
    }
}