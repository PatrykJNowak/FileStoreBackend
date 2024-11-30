namespace FileStore.Api.UseCases.File.GetFile;

public class GetFileDto
{
    public string FileName { get; set; }
    public FileStream Stream { get; set; }
}