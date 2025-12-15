namespace CleanCode.Core.Entities;

public class Order
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public DateTime CreatedAt { get; init; }
    public decimal Total { get; set; }
    
    public ICollection<Product> Products { get; set; }
}
