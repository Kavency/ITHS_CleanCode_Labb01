using CleanCode.Core.Interfaces;

namespace CleanCode.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    Task<int> SaveChangesAsync();
}
