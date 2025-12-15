namespace CleanCode.Application.Models;

public record class OrderResponse
{
    public int OrderId { get; set; }
    public decimal OrderTotal { get; set; }
}
