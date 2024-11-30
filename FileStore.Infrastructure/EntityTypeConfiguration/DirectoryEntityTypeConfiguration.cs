using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Directory = FileStore.Domain.Entities.Directory;

namespace FileStore.Infrastructure.EntityTypeConfiguration;

public class DirectoryEntityTypeConfiguration : IEntityTypeConfiguration<Directory>
{
    public void Configure(EntityTypeBuilder<Directory> builder)
    {
        builder.ToTable("directory");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.Property(x => x.DirectoryName)
            .IsRequired();
        
        builder.Property(x => x.ParentDirectoryId)
            .IsRequired(false);
        
        builder.Property(x => x.OwnerId)
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.File)
            .WithOne(x => x.Directory)
            .HasForeignKey(x => x.Id);
    }
}