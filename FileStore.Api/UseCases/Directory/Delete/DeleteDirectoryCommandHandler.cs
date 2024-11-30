using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.Directory.Delete;

public class DeleteDirectoryCommandHandler : IRequestHandler<DeleteDirectoryCommand, Unit>
{
    private readonly DatabaseContext _dbContext;
    private readonly IFileService _fileService;


    public DeleteDirectoryCommandHandler(DatabaseContext dbContext, IFileService fileService)
    {
        _dbContext = dbContext;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(DeleteDirectoryCommand request, CancellationToken ct)
    {
        var directoryToRemove = await GetDirectoryIds(request.DirectoryId);
        
        var filesToRemove = await _dbContext.File
            .Where(x => directoryToRemove.Contains(x.DirectoryId))
            .Select(x => x.Id)
            .ToListAsync(ct);
        
        await _fileService.DeleteByIdsAsync(filesToRemove);

        await _dbContext.File
            .Where(x => filesToRemove.Contains(x.Id))
            .ExecuteDeleteAsync(ct);

        await _dbContext.Directory
            .Where(x => directoryToRemove.Contains(x.Id))
            .ExecuteDeleteAsync(ct);

        await _dbContext.SaveChangesAsync(ct);

        return Unit.Value;
    }

    private async Task<List<Guid>> GetDirectoryIds(Guid directoryToRemoveId)
    {
        var ids = new List<Guid> { directoryToRemoveId };

        foreach (var directoryId in await _dbContext.Directory.Where(x => x.ParentDirectoryId == directoryToRemoveId).Select(x => x.Id).ToListAsync())
            ids.AddRange(await GetDirectoryIds(directoryId));

        return ids;
    }
}