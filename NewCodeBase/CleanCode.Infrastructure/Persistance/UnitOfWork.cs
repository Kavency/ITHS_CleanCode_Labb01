using CleanCode.Application.Interfaces;
using CleanCode.Core.Interfaces;

namespace CleanCode.Infrastructure.Persistance;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IProductRepository Products { get; }
    public IOrderRepository Orders { get; }
    public IUserRepository Users { get; }
    public ITokenRepository UserTokens { get; }

    public UnitOfWork(AppDbContext context, IProductRepository products, IOrderRepository orders, IUserRepository users, ITokenRepository tokens)
    {
        _context = context;
        Products = products;
        Orders = orders;
        Users = users;
        UserTokens = tokens;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
