using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.File.GetFile;

public class GetFileQueryHandler : IRequestHandler<GetFileQuery, GetFileDto>
{
    private readonly DatabaseContext _dbContext;
    private readonly IFileService _fileService;

    public GetFileQueryHandler(IFileService fileService, DatabaseContext dbContext)
    {
        _fileService = fileService;
        _dbContext = dbContext;
    }

    public async Task<GetFileDto> Handle(GetFileQuery request, CancellationToken ct)
    {
        var file = await _dbContext.File.FirstAsync(x => x.Id == request.FileId, ct);

        return new()
        {
            FileName = file.FileName,
            Stream = await _fileService.GetFileByIdAsync(request.FileId)
        };
    }
}