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

    public async Task<MemoryStream> GetFileByIdAsync(Guid fileId)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> UploadAsync(IFormFile file, CancellationToken ct)
    {
        var fileId = Guid.NewGuid();

        var path = Path.Combine(_storePath, fileId.ToString());

        CreateDirectoryIfNotExists(path);

        await file.CopyToAsync(new FileStream(Path.Combine(path, fileId.ToString()), FileMode.Create), ct);

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