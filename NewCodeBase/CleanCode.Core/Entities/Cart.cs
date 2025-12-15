namespace CleanCode.Core.Entities;

public class Cart
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public DateTime CreatedAt { get; init; }
    public ICollection<CartItem> Items { get; set; }
}
