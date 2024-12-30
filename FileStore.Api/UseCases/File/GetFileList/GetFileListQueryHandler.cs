using FileStore.Api.Extensions;
using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.File.GetFileList;

public class GetFileListQueryHandler : IRequestHandler<GetFileListQuery, List<GetFileListDto>>
{
    private readonly DatabaseContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public GetFileListQueryHandler(DatabaseContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<List<GetFileListDto>> Handle(GetFileListQuery request, CancellationToken ct)
    {
        var response = await _dbContext.File
            .Where(x => x.OwnerId == Guid.Parse(_currentUser.UserId!)
                && x.DirectoryId == request.DirectoryId.GetNullIfGuidIsEmpty())
            .Select(x => new GetFileListDto()
            {
                Id = x.Id,
                FileName = x.FileName,
                FileSize = x.FileSize,
                CreatedAt = x.CreatedAt
            }) 
            .ToListAsync(ct);

        return response;
    }
}