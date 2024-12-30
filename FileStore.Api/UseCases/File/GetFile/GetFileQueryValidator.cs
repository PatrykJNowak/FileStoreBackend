using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.File.GetFile;

public class GetFileQueryValidator : AbstractValidator<GetFileQuery>
{
    private readonly DatabaseContext _dbContext;
    private readonly ICurrentUser _currentUser;
    
    public GetFileQueryValidator(DatabaseContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;

        RuleFor(x => x.FileId)
            .NotEmpty()
            .WithMessage("FileId cannot be empty")
            .MustAsync(DirectoryExists)
            .WithMessage("File not exist");
    }
    
    private async Task<bool> DirectoryExists(Guid fileId, CancellationToken ct)
    {
        var result = await _dbContext.File
            .AnyAsync(x => x.Id == fileId
                && x.OwnerId == Guid.Parse(_currentUser.UserId!), ct);
        return result;
    }
}