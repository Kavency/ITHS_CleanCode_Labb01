namespace CleanCode.Application.Interfaces;

public interface IOrderService
{
    void Get(string? token);
    void Create(string? token);
}
