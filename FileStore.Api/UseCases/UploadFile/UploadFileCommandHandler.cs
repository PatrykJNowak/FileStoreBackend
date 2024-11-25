using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using MediatR;

namespace FileStore.Api.UseCases.UploadFile;

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Unit>
{
    private readonly IFileService _fileService;
    private readonly DatabaseContext _dbContext;

    public UploadFileCommandHandler(IFileService fileService, DatabaseContext dbContext)
    {
        _fileService = fileService;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(UploadFileCommand request, CancellationToken ct)
    {
        var fileId = await _fileService.UploadAsync(request.File, ct);

        await _dbContext.File.AddAsync(new()
        {
            Id = fileId,
            FileSize = (int) (request.File.Length / 1024),
            FileName = request.File.FileName
        }, ct);

        await _dbContext.SaveChangesAsync(ct);
        
        // TODO: add user id here
        
        return Unit.Value;
    }
}