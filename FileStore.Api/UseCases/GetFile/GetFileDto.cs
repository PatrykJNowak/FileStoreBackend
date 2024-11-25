namespace FileStore.Api.UseCases.GetFile;

public class GetFileDto
{
    public string FileName { get; set; }
    public FileStream Stream { get; set; }
}