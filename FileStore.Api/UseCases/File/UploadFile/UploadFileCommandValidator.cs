using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.File.UploadFile;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    private readonly DatabaseContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public UploadFileCommandValidator(DatabaseContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;

        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.DictionaryId)
            .NotEmpty()
            .WithMessage("FileId cannot be empty")
            .MustAsync(DirectoryExists)
            .WithMessage("File not exists");

        RuleFor(x => x.File)
            .NotEmpty()
            .WithMessage("File cannot be empty");
    }
    
    private async Task<bool> DirectoryExists(Guid fileId, CancellationToken ct)
    {
        var result = await _dbContext.Directory
            .AnyAsync(x => x.Id == fileId
                && x.OwnerId == Guid.Parse(_currentUser.UserId!), ct);
        return result;
    }
}