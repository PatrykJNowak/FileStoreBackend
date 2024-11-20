using System.Reflection;
using FileStore.Api.DJ;
using FileStore.Domain.Entity;
using FileStore.Domain.Interfaces;
using FileStore.Infrastructure;
using FileStore.Infrastructure.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {        
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",  // Ensure it's set to "bearer"
        BearerFormat = "JWT",
        Description = "Enter 'Bearer' followed by your token",
    });
    
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                    
                }
            },
            new string[] { } // Empty array means no specific scopes are required
        }
    });
});

var configuration = builder.Configuration;
builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<IdentityDatabaseContext>(opt =>
    opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<IdentityDatabaseContext>()
    .AddApiEndpoints();

builder.Services.Configure<FileStoreSettings>(
    builder.Configuration.GetSection("FileStoreSettings")
);

builder.Services.Configure<FormOptions>(opt =>
{
    opt.MultipartBodyLengthLimit = long.MaxValue;
});

builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    opt.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
    opt.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// app.MapSwagger().RequireAuthorization();
app.MigrateDbContext<DatabaseContext>();
app.MigrateDbContext<IdentityDatabaseContext>();
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();
app.MapControllers();
app.Run();