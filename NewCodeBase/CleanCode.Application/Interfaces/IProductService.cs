using CleanCode.Core.Entities;

namespace CleanCode.Application.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task RemoveAsync(Product product);
    Task UpdateAsync(Product product);
    Task<bool> IncreaseStockAsync(int id, int amount);
    Task<bool> DecreaseStockAsync(int id, int amount);
    Task<List<Product>> SearchAsync(string? query, decimal? maxPrice);
}
