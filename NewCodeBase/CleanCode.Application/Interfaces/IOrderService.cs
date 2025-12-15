using CleanCode.Application.Models;
using CleanCode.Core.Entities;

namespace CleanCode.Application.Interfaces;

public interface IOrderService
{
    Task<OrderResponse> CreateOrderForUserAsync(int userId, CancellationToken ct);
    Task<Order?> GetByIdAsync(int id, CancellationToken ct);
}
