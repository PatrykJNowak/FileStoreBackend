namespace FileStore.Api.UseCases.Directory.GetCurrentUserView;

public class GetCurrentUserViewDto
{
    public Guid Id { get; set; }
    public string DirectoryName { get; set; }
    public DateTime CreatedAt { get; set; }
}