using CleanCode.Core.Entities;

namespace CleanCode.Application.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllAsync(CancellationToken ct);
    Task<Product?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(Product product, CancellationToken ct);
    Task RemoveAsync(Product product, CancellationToken ct);
    Task UpdateAsync(Product product, CancellationToken ct);
    Task<bool> IncreaseStockAsync(int id, int amount, CancellationToken ct);
    Task<bool> DecreaseStockAsync(int id, int amount, CancellationToken ct);
    Task<List<Product>> SearchAsync(string? query, decimal? maxPrice, CancellationToken ct);
}
