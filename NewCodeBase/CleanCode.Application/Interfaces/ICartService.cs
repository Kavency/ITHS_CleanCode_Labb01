using CleanCode.Core.Entities;

namespace CleanCode.Application.Interfaces;

public interface ICartService
{
    Task AddAsync(Cart cart, CancellationToken ct);
}
