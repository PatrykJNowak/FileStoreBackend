using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = FileStore.Domain.Entities.File;

namespace FileStore.Infrastructure.EntityTypeConfiguration;

public class FilesEntityTypeConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable("file");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.DirectoryId)
            .IsRequired();

        builder.Property(x => x.FileSize)
            .IsRequired();

        builder.Property(x => x.FileName)
            .IsRequired();

        builder.Property(x => x.OwnerId)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.Directory)
            .WithMany(x => x.File)
            .HasForeignKey(x => x.Id)
            .IsRequired(false);
    }
}
