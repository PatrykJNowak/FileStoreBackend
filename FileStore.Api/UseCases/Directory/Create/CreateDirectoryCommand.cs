using MediatR;

namespace FileStore.Api.UseCases.Directory.Create;

public class CreateDirectoryCommand : IRequest<Guid>
{
    public Guid? ParentDirectoryId { get; set; }
    public string DirectoryName { get; set; }
}