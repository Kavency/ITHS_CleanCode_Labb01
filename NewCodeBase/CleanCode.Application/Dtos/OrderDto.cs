using CleanCode.Core.Entities;

namespace CleanCode.Application.Dtos;

public class OrderDto
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public DateTime CreatedAt { get; init; }
    public decimal Total { get; set; }
    public ICollection<Product> Products { get; set; } = [];
}
