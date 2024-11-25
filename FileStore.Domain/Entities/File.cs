using FileStore.Domain.Extensions;

namespace FileStore.Domain.Entities;

public class File : Entity
{
    public Guid Id { get; set; }
    public Guid DirectoryId { get; set; }
    public int FileSize { get; set; }
    public string FileName { get; set; }
}