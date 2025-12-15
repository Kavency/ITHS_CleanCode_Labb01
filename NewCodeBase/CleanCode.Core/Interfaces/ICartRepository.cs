using CleanCode.Core.Entities;

namespace CleanCode.Core.Interfaces;

public interface ICartRepository
{
    Task AddAsync(Cart cart, CancellationToken ct);
    Task<Cart> GetByUserIdAsync(int userId, CancellationToken ct);
    void Remove(Cart cart);
    void Update(Cart cart);
}
