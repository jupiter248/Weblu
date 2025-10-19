using System.Text.Json.Serialization;
using DotNetEnv;
using Serilog;
using Weblu.Api.Middlewares;
using Weblu.Api.Extensions;
using Weblu.Application.Extensions;
using Weblu.Infrastructure.Extensions;

Env.Load(Path.Combine("../../.env")); // This loads .env into Environment variables

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();

builder.Host.ApplySerilog();

builder.Services.AddControllersConfigurations();

builder.Services.ConnectToDatabase();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.ConfigureIdentity();


var app = builder.Build();

await app.Services.ApplyMigrations();

app.AddLocalizations();

app.UseMiddleware<ErrorHandlerMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
