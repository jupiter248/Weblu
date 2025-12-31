using DotNetEnv;
using Weblu.Api.Middlewares;
using Weblu.Api.Extensions;
using Weblu.Application.Extensions;
using Weblu.Infrastructure.Extensions;
using Weblu.Api.Extensions.SwaggerConfigurations;

Env.Load(Path.Combine("../../.env")); // This loads .env into Environment variables

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersConfigurations();



builder.Services.ApplyGlobalRateLimiter();
builder.Services.ApplyAuthRateLimiter();
builder.Services.ApplyViewArticleRateLimiter();

builder.Services.ApplyVersioning();
builder.Services.ConfigureSwaggerGen();

builder.Host.ApplySerilog();
builder.Services.ConfigureJwtSettings();
builder.Services.ConnectToDatabase();
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
