using CleanCode.Application.Interfaces;
using CleanCode.Core.Entities;

namespace CleanCode.Application.Services;

public class CartService(IUnitOfWork unitOfWork) : ICartService
{
    public async Task AddAsync(Cart cart, CancellationToken ct)
    {
        await unitOfWork.Carts.AddAsync(cart, ct);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
