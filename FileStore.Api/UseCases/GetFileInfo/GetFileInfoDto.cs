namespace FileStore.Api.UseCases.GetFileInfo;

public class GetFileInfoDto
{
    public Guid Id { get; set; }
    public int FileSize { get; set; }
    public string FileName { get; set; }
}