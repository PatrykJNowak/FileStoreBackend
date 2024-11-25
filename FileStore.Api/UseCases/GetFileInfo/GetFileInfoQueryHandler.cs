using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.GetFileInfo;

public class GetFileInfoQueryHandler : IRequestHandler<GetFileInfoQuery, List<GetFileInfoDto>>
{
    private readonly DatabaseContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public GetFileInfoQueryHandler(DatabaseContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<List<GetFileInfoDto>> Handle(GetFileInfoQuery request, CancellationToken ct)
    {
        var a = _currentUser;
        
        var response = await _dbContext.File.Select(x => new GetFileInfoDto
        {
            Id = x.Id,
            FileName = x.FileName,
            FileSize = x.FileSize,
        }).ToListAsync(ct);

        return response;
    }
}