using CleanCode.Application.Interfaces;
using CleanCode.Core.Entities;

namespace CleanCode.Application.Services;

public class ProductService(IUnitOfWork _unitOfWork) : IProductService
{
    public async Task<List<Product>> GetAllAsync()
    {
        return await _unitOfWork.Products.GetAllAsync();
    }


    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Products.GetByIdAsync(id);
    }


    public async Task AddAsync(Product product)
    {
        _unitOfWork.Products.Add(product);
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task RemoveAsync(Product product)
    {
        _unitOfWork.Products.Remove(product);
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task UpdateAsync(Product product)
    {
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task<bool> IncreaseStockAsync(int id, int amount)
    {
        var product = await GetByIdAsync(id);
        if (product is null) return false;

        product.Stock += amount;
        await _unitOfWork.SaveChangesAsync();
        return true;
    }


    public async Task<bool> DecreaseStockAsync(int id, int amount)
    {
        var product = await GetByIdAsync(id);
        if (product is null || product.Id < amount) return false;

        product.Stock -= amount;
        await _unitOfWork.SaveChangesAsync();
        return true;
    }


    public async Task<List<Product>> SearchAsync(string? query, decimal? maxPrice)
    {
        return await _unitOfWork.Products.SearchAsync(query, maxPrice);
    }
}
