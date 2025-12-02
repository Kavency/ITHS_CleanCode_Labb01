using CleanCode.Core.Entities;

namespace CleanCode.Core.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> SearchAsync(string? query, decimal? maxPrice);
    void Add(Product product);
    void Update(Product product);
    void Remove(Product product);
}
