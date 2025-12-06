
using CleanCode.Application.Interfaces;
using CleanCode.Application.Mapping;
using CleanCode.Application.Services;
using CleanCode.Core.Interfaces;
using CleanCode.Core.Interfaces.Services;
using CleanCode.Infrastructure.Persistance;
using CleanCode.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

string connectionString = "Data Source=../CleanCode.Infrastructure/Persistance/Database/CleanCodeDb.db";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg => 
{ 
    cfg.AddProfile(new ProductProfile()); 
    cfg.AddProfile(new UserProfile());
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();


