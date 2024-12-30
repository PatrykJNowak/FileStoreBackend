using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.Directory.GetCurrentUserView;

public class GetCurrentUserViewQueryHandler : IRequestHandler<GetCurrentUserViewQuery, List<GetCurrentUserViewDto>>
{
    private readonly DatabaseContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public GetCurrentUserViewQueryHandler(DatabaseContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<List<GetCurrentUserViewDto>> Handle(GetCurrentUserViewQuery request, CancellationToken ct)
    {
        var response = await _dbContext.Directory
            .Where(x => x.OwnerId == Guid.Parse(_currentUser.UserId!)
            && x.ParentDirectoryId == request.DirectoryId)
            .Select(x => new GetCurrentUserViewDto()
            {
                Id = x.Id,
                DirectoryName = x.DirectoryName,
                ParentId = x.ParentDirectoryId,                
                CreatedAt = x.CreatedAt
            })
            .ToListAsync(ct);

        return response;
    }
}