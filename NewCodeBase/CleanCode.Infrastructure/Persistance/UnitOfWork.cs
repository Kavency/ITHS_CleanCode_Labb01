using CleanCode.Application.Interfaces;
using CleanCode.Core.Interfaces;

namespace CleanCode.Infrastructure.Persistance;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IProductRepository Products { get; }

    public UnitOfWork(AppDbContext context, IProductRepository products)
    {
        _context = context;
        Products = products;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
