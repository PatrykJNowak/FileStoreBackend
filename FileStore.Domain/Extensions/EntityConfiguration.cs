using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileStore.Domain.Extensions;

public class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(x => x.CreatedBy)
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.UpdatedBy)
            .IsRequired(false);

        builder.Property(x => x.UpdatedAt)
            .ValueGeneratedOnUpdate()
            .IsRequired(false);
    }
}