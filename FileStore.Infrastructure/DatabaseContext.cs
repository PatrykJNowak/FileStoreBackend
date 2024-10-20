using FileStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FileStore.Domain;

public class DatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Files> Tests { get; set; }
}