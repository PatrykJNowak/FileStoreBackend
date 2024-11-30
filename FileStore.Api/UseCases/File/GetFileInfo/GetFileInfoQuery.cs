using MediatR;

namespace FileStore.Api.UseCases.File.GetFileInfo;

public class GetFileInfoQuery : IRequest<List<GetFileInfoDto>>
{
}