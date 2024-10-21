using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.DJ;

public static class HostExtension
{
    public static void MigrateDbContext<TContext>(this IHost host)
        where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<TContext>();

        context.Database.Migrate();
    }
}