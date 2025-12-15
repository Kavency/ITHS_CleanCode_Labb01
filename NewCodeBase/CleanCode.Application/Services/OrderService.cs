using CleanCode.Application.Interfaces;
using CleanCode.Application.Models;
using CleanCode.Core.Entities;

namespace CleanCode.Application.Services;

public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    public async Task<Order?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await unitOfWork.Orders.GetByIdAsync(id, ct);
    }


    public async Task<OrderResponse> CreateOrderForUserAsync(int userId, CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync(ct);

        try
        {
            var cart = await unitOfWork.Carts.GetByUserIdAsync(userId, ct) ?? throw new Exception("Cart not found");
            var products = new List<Product>();
            decimal total = 0.0m;

            foreach (var item in cart.Items)
            {
                var product = await unitOfWork.Products.GetByIdAsync(item.ProductId, ct) ?? throw new Exception("Failure fetching product.");
                if (product.Stock < item.Quantity) throw new Exception($"Not enough in stock for {product.Name}");

                product.Stock -= item.Quantity;
                unitOfWork.Products.Update(product);

                products.Add(product);
                total += product.Price * item.Quantity;
            }
            
            var order = new Order { CreatedAt = DateTime.Now, UserId = userId, Total = total, Products = products };

            unitOfWork.Orders.Create(order);
            unitOfWork.Carts.Remove(cart);
            
            await unitOfWork.CommitAsync(ct);

            return new OrderResponse { OrderId = 1, OrderTotal = total }; 
        }
        catch
        {
            await unitOfWork.RollbackAsync(ct);
            throw;
        }
    }
}
