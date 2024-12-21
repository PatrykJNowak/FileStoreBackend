namespace FileStore.Api.UseCases.File.GetUserUsedSize;

public class GetUserUsedSizeDto
{
    public int MaxSize { get; set; }
    public int File { get; set; }
    public int PrecentageFilledIn { get; set; }
}