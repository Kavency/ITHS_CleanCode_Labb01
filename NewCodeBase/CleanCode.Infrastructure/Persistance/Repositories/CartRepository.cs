using CleanCode.Core.Entities;
using CleanCode.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleanCode.Infrastructure.Persistance.Repositories;

public class CartRepository(AppDbContext context) : ICartRepository
{
    public async Task<Cart> GetByUserIdAsync(int userId, CancellationToken ct)
    {
        return await context.Carts
            .Include(c => c.Items)
            .FirstAsync(c => c.UserId == userId);
    }

    public async Task AddAsync(Cart cart, CancellationToken ct) => await context.Carts.AddAsync(cart, ct);
    public void Update(Cart cart) => context.Carts.Update(cart);
    public void Remove(Cart cart) => context.Carts.Remove(cart);
}
