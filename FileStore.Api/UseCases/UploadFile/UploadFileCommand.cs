using MediatR;

namespace FileStore.Api.UseCases.UploadFile;

public class UploadFileCommand : IRequest<Unit>
{
    public IFormFile File { get; set; }
}