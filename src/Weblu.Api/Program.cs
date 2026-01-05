using DotNetEnv;
using Weblu.Application.Extensions;
using Weblu.Infrastructure.Extensions;
using Weblu.Api.Extensions;
using Weblu.Api.Extensions.SwaggerConfigurations;
using Weblu.Api.Middlewares;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;

var envPath = Path.GetFullPath(
    Path.Combine(env.ContentRootPath, "..", "..", ".env")
);

Env.Load(envPath);

builder.Services.AddControllersConfigurations();
builder.Services.AddEndpointsApiExplorer();


builder.Host.ApplySerilog();

builder.Services.ApplyGlobalRateLimiter();
builder.Services.ApplyAuthRateLimiter();
builder.Services.ApplyViewArticleRateLimiter();

builder.Services.ApplyVersioning();
builder.Services.ConfigureSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");


if (!string.IsNullOrWhiteSpace(connectionString))
{
    builder.Services.AddDatabase(connectionString);
}

builder.Services.ConfigureJwtSettings();
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
    app.MapScalarApiReference(options =>
    {
        options.UseScalarWithVersions(app);
    });
}

app.UseRateLimiter();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();