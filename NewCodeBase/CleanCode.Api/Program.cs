
using CleanCode.Application.Mapping;
using CleanCode.Core.Interfaces;
using CleanCode.Infrastructure.Persistance;
using CleanCode.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

string connectionString = "Data Source=../CleanCode.Infrastructure/Persistance/Database/CleanCodeDb.db";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg => { cfg.AddProfile(new ProductProfile()); });
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();


