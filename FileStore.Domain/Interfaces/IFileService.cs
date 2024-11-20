using Microsoft.AspNetCore.Http;

namespace FileStore.Domain.Interfaces;

public interface IFileService
{
    public Task<MemoryStream> GetFileByIdAsync(Guid fileId, CancellationToken ct);
    public Task<Guid> UploadAsync(IFormFile file, CancellationToken ct);
    public Task DeleteByIdAsync(Guid fileId);
    
}