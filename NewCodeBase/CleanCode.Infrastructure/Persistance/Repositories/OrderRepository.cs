using CleanCode.Core.Entities;
using CleanCode.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleanCode.Infrastructure.Persistance.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
   public async Task<Order?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await context.Orders.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    
    public void Create(Order order) => context.Orders.Add(order);
    public void Update(Order order) => context.Orders.Update(order);
    public void Remove(Order order) => context.Orders.Remove(order);
}
