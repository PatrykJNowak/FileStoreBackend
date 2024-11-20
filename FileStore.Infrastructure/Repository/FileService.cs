using FileStore.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FileStore.Infrastructure.Repository;

public class FileService : IFileService
{
    private readonly string _storePath;

    public FileService(IOptions<FileStoreSettings> options)
    {
        _storePath = options.Value.StorePath; // Get the StorePath from the settings
        CreateDirectoryIfNotExists(_storePath);
    }

    public async Task<MemoryStream> GetFileByIdAsync(Guid fileId, CancellationToken ct)
    {
        var filePath = Path.Combine(_storePath, fileId.ToString());

        var memoryStream = new MemoryStream();

        await using (var fileStream = new FileStream(Path.Combine(filePath, fileId.ToString()), FileMode.Open, FileAccess.Read))
        {
            await fileStream.CopyToAsync(memoryStream, ct);
        }

        memoryStream.Position = 0;
        return memoryStream;
    }

    public async Task<Guid> UploadAsync(IFormFile file, CancellationToken ct)
    {
        var fileId = Guid.NewGuid();

        var filePath = Path.Combine(_storePath, fileId.ToString());

        CreateDirectoryIfNotExists(filePath);

        await using (var fileStream = new FileStream(Path.Combine(filePath, fileId.ToString()), FileMode.Create, FileAccess.Write))
        {
            await file.CopyToAsync(fileStream, ct);
        }
        
        return fileId;
    }

    public async Task DeleteByIdAsync(Guid fileId)
    {
        throw new NotImplementedException();
    }

    private void CreateDirectoryIfNotExists(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
    }
}