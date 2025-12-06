using CleanCode.Core.Interfaces;

namespace CleanCode.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
    IUserRepository Users { get; }
    ITokenRepository UserTokens { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}
