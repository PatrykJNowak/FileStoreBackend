using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.File.DeleteFile;

public class DeleteFileCommandValidator : AbstractValidator<DeleteFileCommand>
{
    private readonly DatabaseContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public DeleteFileCommandValidator(DatabaseContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;

        RuleFor(x => x.FileId)
            .NotEmpty()
            .WithMessage("FileId cannot be empty")
            .MustAsync(FileExist)
            .WithMessage("File not exist")
            .MustAsync(UserCanRemoveFile)
            .WithMessage("Prohibited operation! You can not remove other user file");
    }

    private async Task<bool> UserCanRemoveFile(Guid fileId, CancellationToken ct)
    {
        var result = await _dbContext.File.AnyAsync(
            x => x.Id == fileId && 
                x.OwnerId == Guid.Parse(_currentUser.UserId!), ct);
        return result;
    }
 
    private async Task<bool> FileExist(Guid fileId, CancellationToken ct)
    {
        var result = await _dbContext.File
            .AnyAsync(x => x.Id == fileId, ct);
        return result;
    }
}