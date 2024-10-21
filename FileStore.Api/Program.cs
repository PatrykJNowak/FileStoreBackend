using System.Reflection;
using FileStore.Api.DJ;
using FileStore.Domain.Entity;
using FileStore.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var configuration = builder.Configuration;
builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<IdentityDatabaseContext>()
    .AddApiEndpoints();

builder.Services.AddDbContext<IdentityDatabaseContext>(opt =>
    opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    opt.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
    opt.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

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