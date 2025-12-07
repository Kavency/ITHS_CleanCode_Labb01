using CleanCode.Core.Entities;
using CleanCode.Core.Interfaces;
using CleanCode.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CleanCode.Infrastructure.Repositories;

public class ProductRepository(AppDbContext _context) : IProductRepository
{
    public async Task<List<Product>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Products.ToListAsync(ct);
    }


    public async Task<Product?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _context.Products.FirstOrDefaultAsync(x => x.Id == id, ct);
    }


    public async Task<List<Product>> SearchAsync(string? query, decimal? maxPrice, CancellationToken ct)
    {
        var products = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            var q = query.ToLowerInvariant();
            products = products.Where(p => p.Name.ToLower().Contains(q));

            if (decimal.TryParse(query, out var priceQuery))
                products = products.Where(p => p.Price == priceQuery);
        }

        if (maxPrice.HasValue)
            products = products.Where(p => p.Price <= maxPrice.Value);

        return await products.ToListAsync();
    }


    public void Add(Product product) => _context.Products.Add(product);
    public void Remove(Product product) => _context.Products.Remove(product);
    public void Update(Product product) => _context.Products.Update(product);
}
