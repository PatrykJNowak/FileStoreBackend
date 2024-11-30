using MediatR;

namespace FileStore.Api.UseCases.File.UploadFile;

public class UploadFileCommand : IRequest<Unit>
{
    public IFormFile File { get; set; }
    public Guid DictionaryId { get; set; }
}