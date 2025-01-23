using FileStore.Api.DJ;
using FileStore.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.Directory.Create;

public class CreateDirectoryCommandValidator : AbstractValidator<CreateDirectoryCommand>
{
    private readonly DatabaseContext _dbContext;
    public CreateDirectoryCommandValidator(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
        
        RuleFor(x => x.ParentDirectoryId)
            .NotEmpty()
            .WithValidationError("Id cannot be null or empty")
            .MustAsync(DirectoryExists)
            .When(x => x.ParentDirectoryId != null)
            .WithValidationError("Directory doesn't exist");

        RuleFor(x => x.DirectoryName)
            .NotEmpty()
            .WithValidationError("Directory name cannot be empty");
    }
    
    private async Task<bool> DirectoryExists(Guid? directoryId, CancellationToken ct)
    {
        var result = await _dbContext.Directory.AnyAsync(x => x.Id == directoryId, ct);
        return result;
    }
}