using FileStore.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.GetFileInfo;

public class GetFileInfoQueryHandler : IRequestHandler<GetFileInfoQuery, List<GetFileInfoDto>>
{
    private readonly DatabaseContext _dbContext;

    public GetFileInfoQueryHandler(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetFileInfoDto>> Handle(GetFileInfoQuery request, CancellationToken ct)
    {
        var response = await _dbContext.File.Select(x => new GetFileInfoDto
        {
            Id = x.Id,
            FileName = x.FileName,
            FileSize = x.FileSize,
        }).ToListAsync(ct);

        return response;
    }
}