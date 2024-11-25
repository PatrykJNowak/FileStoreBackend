using FileStore.Domain.Extensions;
using FileStore.Domain.IdentityEntity;
using FileStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using File = FileStore.Domain.Entities.File;

namespace FileStore.Infrastructure;

public class DatabaseContext : DbContext
{
    private readonly ICurrentUser _currentUser;
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options, ICurrentUser currentUser) : base(options)
    {
        _currentUser = currentUser;
    }

    public DbSet<User> IdentityUsers { get; set; }
    public DbSet<File> File { get; set; }
    
    // ------------,-------------
    // Db context extensions
    // -------------------------
    
    public new async Task<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : Entity
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = Guid.Parse(_currentUser.UserId!);
        
        return await base.AddAsync(entity, cancellationToken);
    }
    
    public new async Task<EntityEntry<TEntity>> Add<TEntity>(TEntity entity) where TEntity : Entity
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = Guid.Parse(_currentUser.UserId!);
        
        return base.Add(entity);
    }
    
    public new async Task<EntityEntry<TEntity>> Update<TEntity>(TEntity entity) where TEntity : Entity
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = Guid.Parse(_currentUser.UserId!);
        
        return base.Update(entity);
    }
}