using CleanCode.Core.Entities;

namespace CleanCode.Core.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id, CancellationToken ct);
    void Create(Order order);
    void Update(Order order);
    void Remove(Order order);
}
