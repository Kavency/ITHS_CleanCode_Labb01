using CleanCode.Core.Entities;

namespace CleanCode.Core.Interfaces;

public interface IOrderRepository
{
    Task<Order?> Get(int id);
    void Create(Order order);
}
