namespace FileStore.Domain.Interfaces;

public interface ICurrentUser
{
    public string? UserId { get; }
    public string? UserName { get; }
}