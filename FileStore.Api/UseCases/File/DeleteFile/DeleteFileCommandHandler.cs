using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.File.DeleteFile;

public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, Unit>
{
    private readonly DatabaseContext _dbContext;
    private readonly IFileService _fileService;

    public DeleteFileCommandHandler(DatabaseContext dbContext, IFileService fileService)
    {
        _dbContext = dbContext;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken ct)
    {
        var file = await _dbContext.File.FirstAsync(x => x.Id == request.FileId, ct);

        await _fileService.DeleteByIdAsync(file.Id);

        await _dbContext.File.Where(x => x.Id == file.Id).ExecuteDeleteAsync(ct);

        await _dbContext.SaveChangesAsync(ct);

        return Unit.Value;
    }
}