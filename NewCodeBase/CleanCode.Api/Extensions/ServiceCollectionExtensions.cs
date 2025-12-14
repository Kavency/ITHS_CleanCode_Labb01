using CleanCode.Application.Interfaces;
using CleanCode.Application.Mapping;
using CleanCode.Application.Services;
using CleanCode.Core.Interfaces;
using CleanCode.Infrastructure.Persistance;
using CleanCode.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanCode.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppDiServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IServiceFacade, ServiceFacade>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ICartRepository, CartRepository>();

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
            cfg.AddProfile(new CartProfile());
            cfg.AddProfile(new OrderProfile());
        });

        return services;
    }
}
