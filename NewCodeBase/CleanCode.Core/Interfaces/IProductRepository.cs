using CleanCode.Core.Entities;

namespace CleanCode.Core.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync(CancellationToken ct);
    Task<Product?> GetByIdAsync(int id, CancellationToken ct);
    Task<List<Product>> SearchAsync(string? query, decimal? maxPrice, CancellationToken ct);
    void Add(Product product);
    void Update(Product product);
    void Remove(Product product);
}
