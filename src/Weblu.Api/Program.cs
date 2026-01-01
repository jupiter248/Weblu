using DotNetEnv;
using Weblu.Application.Extensions;
using Weblu.Infrastructure.Extensions;
using Weblu.Api.Extensions;
using Weblu.Api.Extensions.SwaggerConfigurations;

using Weblu.Api.Middlewares;

var envPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".env");
Env.Load(envPath); // This loads .env into Environment variables

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersConfigurations();
builder.Services.AddEndpointsApiExplorer();


builder.Host.ApplySerilog();

builder.Services.AddControllers();

builder.Services.ApplyGlobalRateLimiter();
builder.Services.ApplyAuthRateLimiter();
builder.Services.ApplyViewArticleRateLimiter();

builder.Services.ApplyVersioning();
builder.Services.ConfigureSwaggerGen();

builder.Host.ApplySerilog();

var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");

builder.Services.ConfigureJwtSettings();

if (!string.IsNullOrWhiteSpace(connectionString))
{
    builder.Services.AddDatabase(connectionString);
}
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt();


builder.Services.ApplyCors();


var app = builder.Build();

await app.Services.ApplyMigrations();

app.AddLocalizations();

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithVersions();
}

app.UseRateLimiter();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Make the implicit Program class public so test projects can access it
// public partial class Program { }