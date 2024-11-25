using MediatR;

namespace FileStore.Api.UseCases.DeleteFile;

public class DeleteFileCommand : IRequest<Unit>
{
    public Guid FileId { get; set; }
}