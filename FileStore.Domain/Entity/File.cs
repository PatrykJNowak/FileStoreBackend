namespace FileStore.Domain.Entity;

public class File
{
    public Guid Id { get; set; }
    public int FileSize { get; set; }
    public string FileName { get; set; }
}