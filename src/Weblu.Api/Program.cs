using System.Text.Json.Serialization;
using DotNetEnv;
using Serilog;
using Weblu.Api.Middlewares;
using Weblu.Infrastructure.Extensions;

Env.Load(Path.Combine("../../.env")); // This loads .env into Environment variables

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341") // Seq running in Docker
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // This disables the default automatic 400 response
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.ConnectToDatabase();
builder.Services.AddInfrastructure();

var app = builder.Build();

var supportedCultures = new[] { "en", "fa" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Services.ApplyMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
