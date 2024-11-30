using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.File.GetFileInfo;

public class GetFileInfoQueryHandler : IRequestHandler<GetFileInfoQuery, List<GetFileInfoDto>>
{
    private readonly ICurrentUser _currentUser;
    private readonly DatabaseContext _dbContext;

    public GetFileInfoQueryHandler(DatabaseContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<List<GetFileInfoDto>> Handle(GetFileInfoQuery request, CancellationToken ct)
    {
        var response = await _dbContext.File.Select(x => new GetFileInfoDto
        {
            Id = x.Id,
            FileName = x.FileName,
            FileSize = x.FileSize
        }).ToListAsync(ct);

        return response;
    }
}