using MediatR;

namespace FileStore.Api.UseCases.File.DeleteFile;

public class DeleteFileCommand : IRequest<Unit>
{
    public Guid FileId { get; set; }
}