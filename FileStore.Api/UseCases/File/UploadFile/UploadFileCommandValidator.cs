using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.File.UploadFile;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    private readonly DatabaseContext _dbContext;
    private readonly ICurrentUser _currentUser;
    private readonly IConfiguration _configuration;

    public UploadFileCommandValidator(DatabaseContext dbContext, ICurrentUser currentUser, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
        _configuration = configuration;

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

        RuleFor(x => x)
            .MustAsync(UserHaveEnoughSpaceToUploadFile)
            .WithMessage("Not enough space to upload file");
    }

    private async Task<bool> DirectoryExists(Guid fileId, CancellationToken ct)
    {
        var result = await _dbContext.Directory
            .AnyAsync(x => x.Id == fileId
                && x.OwnerId == Guid.Parse(_currentUser.UserId!), ct);
        return result;
    }

    private async Task<bool> UserHaveEnoughSpaceToUploadFile(UploadFileCommand fileId, CancellationToken ct)
    {
        var result = await _dbContext.File
            .Where(x => x.OwnerId == Guid.Parse(_currentUser.UserId!))
            .SumAsync(x => x.FileSize, ct);

        return _configuration.GetValue<int>("UserSpaceLimit") > result;
    }
}