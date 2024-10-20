using Microsoft.EntityFrameworkCore;

namespace FileStore.Api.DJ;

public static class IHostExtension
{
    public static void MigrateDbContext<TContext>(this IHost host)
        where TContext : DbContext
    {
        var task = Task.Run(() =>
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<TContext>();

            context.Database.SetCommandTimeout(0);
            context.Database.Migrate();
        });

        task.GetAwaiter().GetResult();
    }
}