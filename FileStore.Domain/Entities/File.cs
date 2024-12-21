namespace FileStore.Domain.Entities;

public class File
{
    public Guid Id { get; set; }
    public virtual Directory Directory { get; set; }
    public Guid? DirectoryId { get; set; }
    public int FileSize { get; set; }
    public string FileName { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }
}
