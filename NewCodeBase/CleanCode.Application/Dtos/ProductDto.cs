using CleanCode.Core.Entities;

namespace CleanCode.Application.Dtos;

public class ProductDto
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
