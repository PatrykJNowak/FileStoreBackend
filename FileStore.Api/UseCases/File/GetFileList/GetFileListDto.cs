namespace FileStore.Api.UseCases.File.GetFileList;

public class GetFileListDto
{
    public Guid Id { get; set; }
    public int FileSize { get; set; }
    public string FileName { get; set; }
    public DateTime CreatedAt { get; set; }
}