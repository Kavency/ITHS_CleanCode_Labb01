using CleanCode.Core.Entities;
using CleanCode.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleanCode.Infrastructure.Persistance;

public class OrderRepository(AppDbContext _context) : IOrderRepository
{
    public async Task<Order?> Get(int id)
    {
        return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    
    public void Create(Order order) => _context.Orders.Add(order);
}
