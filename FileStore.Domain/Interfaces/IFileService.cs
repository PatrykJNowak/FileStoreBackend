using Microsoft.AspNetCore.Http;

namespace FileStore.Domain.Interfaces;

public interface IFileService
{
    public Task<FileStream> GetFileByIdAsync(Guid fileId);
    public Task<Guid> UploadAsync(IFormFile file, CancellationToken ct);
    public Task DeleteByIdAsync(Guid fileId);
    
}