using Directorio.Api.Data;
using Directorio.Api.Repositories;
using Directorio.Api.Repositories.Interfaces;
using Directorio.Api.Services;
using Directorio.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<DirectorioDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IDirectorioService, DirectorioService>();

builder.Services.AddControllers();

// ======== APP ========

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DirectorioDbContext>();
    dbContext.Database.EnsureCreated();
}

app.MapControllers();

app.Run();
