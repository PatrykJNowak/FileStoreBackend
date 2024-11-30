using FileStore.Api.DJ;
using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.UseCases.Directory.GetCurrentUserView;

public class GetCurrentUserViewQueryValidator : AbstractValidator<GetCurrentUserViewQuery>
{
    private readonly DatabaseContext _dbContext;
    private readonly ICurrentUser _currentUser;
    public GetCurrentUserViewQueryValidator(DatabaseContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;

        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.DirectoryId)
            .NotEmpty()
            .WithValidationError("Id cannot be null or empty")
            .MustAsync(DirectoryExists)
            .When(x => x.DirectoryId is not null)
            .WithValidationError("Directory doesn't exist")
            .MustAsync(UserCanDeleteDirectory)
            .When(x => x.DirectoryId is not null)
            .WithValidationError("Prohibited operation");

    }

    private async Task<bool> DirectoryExists(Guid? directoryId, CancellationToken ct)
    {
        var result = await _dbContext.Directory.AnyAsync(x => x.Id == directoryId, ct);
        return result;
    }

    private async Task<bool> UserCanDeleteDirectory(Guid? directoryId, CancellationToken ct)
    {
        var result = await _dbContext.Directory
            .AnyAsync(x => x.Id == directoryId
                && x.OwnerId == Guid.Parse(_currentUser.UserId!), ct);
        return result;
    }
}