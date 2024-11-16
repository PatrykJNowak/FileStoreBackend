using FileStore.Domain.Entity;

namespace FileStore.Infrastructure.Interfaces;

public interface ICurrentUser
{
    public string? UserId { get; }
    public string? Username { get; }
}