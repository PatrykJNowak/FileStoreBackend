using FileStore.Api.Extensions;
using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using MediatR;

namespace FileStore.Api.UseCases.File.UploadFile;

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Unit>
{
    private readonly ICurrentUser _currentUser;
    private readonly DatabaseContext _dbContext;
    private readonly IFileService _fileService;

    public UploadFileCommandHandler(IFileService fileService, DatabaseContext dbContext, ICurrentUser currentUser)
    {
        _fileService = fileService;
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(UploadFileCommand request, CancellationToken ct)
    {
        var fileId = await _fileService.UploadAsync(request.File, ct);

        try
        {
            await _dbContext.File.AddAsync(new()
            {
                Id = fileId,
                DirectoryId = request.DirectoryId.GetNullIfGuidIsEmpty(),
                FileSize = (int) (request.File.Length / 1024),
                FileName = request.File.FileName,
                OwnerId = Guid.Parse(_currentUser.UserId!),
                CreatedAt = DateTime.UtcNow
            }, ct);

            await _dbContext.SaveChangesAsync(ct);
        }
        catch
        {
            await _fileService.DeleteByIdAsync(fileId);
        }


        return Unit.Value;
    }
}