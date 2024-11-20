using MediatR;

namespace FileStore.Api.UseCases.GetFile;

public class GetFileQuery : IRequest<GetFileDto>
{
    public Guid FileId { get; set; }
}