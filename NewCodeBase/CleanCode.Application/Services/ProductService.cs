using CleanCode.Application.Interfaces;
using CleanCode.Core.Entities;

namespace CleanCode.Application.Services;

public class ProductService(IUnitOfWork _unitOfWork) : IProductService
{
    public async Task<List<Product>> GetAllAsync(CancellationToken ct)
    {
        return await _unitOfWork.Products.GetAllAsync(ct);
    }


    public async Task<Product?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _unitOfWork.Products.GetByIdAsync(id, ct);
    }


    public async Task AddAsync(Product product, CancellationToken ct)
    {
        _unitOfWork.Products.Add(product);
        await _unitOfWork.SaveChangesAsync(ct);
    }


    public async Task RemoveAsync(Product product, CancellationToken ct)
    {
        _unitOfWork.Products.Remove(product);
        await _unitOfWork.SaveChangesAsync(ct);
    }


    public async Task UpdateAsync(Product product, CancellationToken ct)
    {
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync(ct);
    }


    public async Task<bool> IncreaseStockAsync(int id, int amount, CancellationToken ct)
    {
        var product = await GetByIdAsync(id, ct);
        if (product is null) return false;

        product.Stock += amount;
        await _unitOfWork.SaveChangesAsync(ct);
        return true;
    }


    public async Task<bool> DecreaseStockAsync(int id, int amount, CancellationToken ct)
    {
        var product = await GetByIdAsync(id, ct);
        if (product is null || product.Id < amount) return false;

        product.Stock -= amount;
        await _unitOfWork.SaveChangesAsync(ct);
        return true;
    }


    public async Task<List<Product>> SearchAsync(string? query, decimal? maxPrice, CancellationToken ct)
    {
        return await _unitOfWork.Products.SearchAsync(query, maxPrice, ct);
    }
}
