using MediatR;

namespace FileStore.Api.UseCases.File.GetFile;

public class GetFileQuery : IRequest<GetFileDto>
{
    public Guid FileId { get; set; }
}