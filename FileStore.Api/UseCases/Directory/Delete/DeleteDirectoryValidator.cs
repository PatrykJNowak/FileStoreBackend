using FileStore.Api.DJ;
using FileStore.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.Directory.Delete;

public class DeleteDirectoryValidator : AbstractValidator<DeleteDirectoryCommand>
{
    private readonly DatabaseContext _dbContext;
    public DeleteDirectoryValidator(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
        
        RuleFor(x => x.DirectoryId)
            .NotEmpty()
            .WithValidationError("Id cannot be null or empty")
            .MustAsync(DirectoryExists)
            .WithValidationError("Directory doesn't exist");
    }

    private async Task<bool> DirectoryExists(Guid directoryId, CancellationToken ct)
    {
        var result = await _dbContext.Directory.AnyAsync(x => x.Id == directoryId, ct);
        return result;
    }
}