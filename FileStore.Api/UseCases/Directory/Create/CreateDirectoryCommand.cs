using MediatR;

namespace FileStore.Api.UseCases.Directory.Create;

public class CreateDirectoryCommand : IRequest<Unit>
{
    public Guid? ParentDirectoryId { get; set; }
    public string DirectoryName { get; set; }
}