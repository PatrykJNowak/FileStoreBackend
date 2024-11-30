using MediatR;

namespace FileStore.Api.UseCases.File.GetFileList;

public class GetFileListQuery : IRequest<List<GetFileListDto>>
{
    public Guid DirectoryId { get; set; }
}