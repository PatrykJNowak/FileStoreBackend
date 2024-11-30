using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using MediatR;

namespace FileStore.Api.UseCases.Directory.Create;

public class CreateDirectoryCommandHandler : IRequestHandler<CreateDirectoryCommand, Guid>
{
    private readonly DatabaseContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public CreateDirectoryCommandHandler(DatabaseContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(CreateDirectoryCommand request, CancellationToken ct)
    {
        var newDirectoryId = Guid.NewGuid();
        
        await _dbContext.Directory.AddAsync(new()
        {
            Id = newDirectoryId,
            DirectoryName = request.DirectoryName,
            ParentDirectoryId = request.ParentDirectoryId,
            CreatedAt = DateTime.UtcNow,
            OwnerId = Guid.Parse(_currentUser.UserId!),
        }, ct);

        await _dbContext.SaveChangesAsync(ct);

        return newDirectoryId;
    }
}