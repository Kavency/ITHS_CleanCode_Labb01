using CleanCode.Application.Interfaces;
using CleanCode.Application.Mapping;
using CleanCode.Application.Services;
using CleanCode.Core.Interfaces;
using CleanCode.Infrastructure.Persistance;
using CleanCode.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanCode.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppDiServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }


    public static IServiceCollection AddSqlDatabaseServices(this IServiceCollection services)
    {
        string connectionString = "Data Source=../CleanCode.Infrastructure/Persistance/Database/CleanCodeDb.db";

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        return services;
    }


    public static IServiceCollection AddAutoMapperProfileServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(new ProductProfile());
            cfg.AddProfile(new UserProfile());
        });

        return services;
    }
}
