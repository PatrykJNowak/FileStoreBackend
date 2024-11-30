using MediatR;

namespace FileStore.Api.UseCases.Directory.Delete;

public class DeleteDirectoryCommand : IRequest<Unit>
{
    public Guid DirectoryId { get; set; }
}