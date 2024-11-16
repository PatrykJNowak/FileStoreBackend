using System.Security.Claims;
using FileStore.Infrastructure.Interfaces;

namespace FileStore.Api.Repository;

public class CurrentUser : ICurrentUser
{
    public string? UserId { get; }
    public string? Username { get; }

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User;

        if (claimsPrincipal is null) 
            return;

        UserId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        UserId = claimsPrincipal.FindFirstValue(ClaimTypes.Name);
    }
}