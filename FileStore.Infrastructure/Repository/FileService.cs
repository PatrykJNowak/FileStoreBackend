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
        if (!Directory.Exists(_storePath))
            Directory.CreateDirectory(_storePath);
    }

    public async Task<FileStream> GetFileByIdAsync(Guid fileId)
    {
        return new FileStream(CreateFilePath(fileId), FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    public async Task<Guid> UploadAsync(IFormFile file, CancellationToken ct)
    {
        var fileId = Guid.NewGuid();

        CreateDirectoryIfNotExists(fileId);

        await using var fileStream = new FileStream(CreateFilePath(fileId), FileMode.Create, FileAccess.Write);
        await file.CopyToAsync(fileStream, ct);

        return fileId;
    }

    public async Task DeleteByIdAsync(Guid fileId)
    {
        if (FileExists(fileId))
            Directory.Delete(CreateDirectoryPath(fileId), true);
    }

    private bool FileExists(Guid fileId)
    {
        var filePath = Path.Combine(_storePath, fileId.ToString(), fileId.ToString());

        return File.Exists(filePath);
    }

    private void CreateDirectoryIfNotExists(Guid fileId)
    {
        var directoryPath = Path.Combine(_storePath, fileId.ToString());

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
    }

    private string CreateFilePath(Guid fileId)
    {
        return Path.Combine(_storePath, fileId.ToString(), fileId.ToString());
    }

    private string CreateDirectoryPath(Guid fileId)
    {
        return Path.Combine(_storePath, fileId.ToString());
    }
}