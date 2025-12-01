using CleanCode.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

string connectionString = "Data Source=../CleanCode.Infrastructure/Persistance/Database/CleanCodeDb.db";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();


