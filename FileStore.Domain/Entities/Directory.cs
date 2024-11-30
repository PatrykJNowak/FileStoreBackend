namespace FileStore.Domain.Entities;

public class Directory
{
    public Guid Id { get; set; }
    public string DirectoryName { get; set; }
    public Guid? ParentDirectoryId { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public virtual ICollection<File> File { get; set; }
}