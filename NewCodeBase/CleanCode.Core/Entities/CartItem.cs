namespace CleanCode.Core.Entities;

public class CartItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public Cart Cart { get; set; } = new();
    
    public int ProductId { get; set; }
    public Product Product { get; set; } = new();
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
