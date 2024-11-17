using System.Security.Claims;
using FileStore.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FileStore.Infrastructure.Repository;

public class CurrentUser : ICurrentUser
{
    public string? UserId { get; }
    public string? UserName { get; }

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User;

        if (claimsPrincipal is null) 
            return;

        UserId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        UserName = claimsPrincipal.FindFirstValue(ClaimTypes.Name);
    }
}