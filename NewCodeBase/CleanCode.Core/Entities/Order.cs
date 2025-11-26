namespace CleanCode.Core.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Total { get; set; }
    public List<Product> Products { get; set; } = [];
}
