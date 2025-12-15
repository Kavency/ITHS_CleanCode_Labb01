using CleanCode.Application.Interfaces;
using CleanCode.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CleanCode.Infrastructure.Persistance;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;
    private bool _disposed;
    public IProductRepository Products { get; }
    public IOrderRepository Orders { get; }
    public IUserRepository Users { get; }
    public ITokenRepository UserTokens { get; }
    public ICartRepository Carts { get; }

    public UnitOfWork(AppDbContext context, 
        IProductRepository products, 
        IOrderRepository orders, 
        IUserRepository users, 
        ITokenRepository tokens,
        ICartRepository carts)
    {
        _context = context;
        Products = products;
        Orders = orders;
        Users = users;
        UserTokens = tokens;
        Carts = carts;
    }

    
    public async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct);
    }


    public async Task BeginTransactionAsync(CancellationToken ct)
    {
        _transaction = await _context.Database.BeginTransactionAsync(ct);
    }


    public async Task CommitAsync(CancellationToken ct)
    {
        await _context.SaveChangesAsync(ct);
        await _transaction.CommitAsync(ct);
    }


    public async Task RollbackAsync(CancellationToken ct)
    {
        _transaction?.RollbackAsync(ct);
    }

    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
