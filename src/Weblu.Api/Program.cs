using DotNetEnv;
using Weblu.Infrastructure.Extensions;

Env.Load(Path.Combine("../../.env")); // This loads .env into Environment variables

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConnectToDatabase();

var app = builder.Build();

app.Services.ApplyMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
