using MediatR;

namespace FileStore.Api.UseCases.Directory.GetCurrentUserView;

public class GetCurrentUserViewQuery : IRequest<List<GetCurrentUserViewDto>>
{
    public Guid? DirectoryId { get; set; }
}