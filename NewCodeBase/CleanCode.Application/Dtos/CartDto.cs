using CleanCode.Core.Entities;

namespace CleanCode.Application.Dtos;

public class CartDto
{
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<CartItem> Items { get; set; }
}
