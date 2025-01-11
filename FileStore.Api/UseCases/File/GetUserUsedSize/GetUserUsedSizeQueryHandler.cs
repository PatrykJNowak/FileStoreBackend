using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.File.GetUserUsedSize;

public class GetUserUsedSizeQueryHandler : IRequestHandler<GetUserUsedSizeQuery, GetUserUsedSizeDto>
{
    private readonly DatabaseContext _dbContext;
    private readonly ICurrentUser _currentUser;
    private readonly IConfiguration _configuration;

    public GetUserUsedSizeQueryHandler(DatabaseContext dbContext, ICurrentUser currentUser, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
        _configuration = configuration;
    }

    public async Task<GetUserUsedSizeDto> Handle(GetUserUsedSizeQuery request, CancellationToken ct)
    {
        var usedSize = await _dbContext.File
            .Where(x => x.OwnerId == Guid.Parse(_currentUser.UserId!))
            .SumAsync(x => x.FileSize, ct);

        return new()
        {
            File = usedSize,
            MaxSize = _configuration.GetValue<int>("UserSpaceLimit"),
            PrecentageFilledIn = usedSize / _configuration.GetValue<double>("UserSpaceLimit")
        };
    }
}