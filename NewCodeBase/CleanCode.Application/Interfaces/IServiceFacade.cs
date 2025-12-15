namespace CleanCode.Application.Interfaces;

public interface IServiceFacade
{
    IOrderService OrderService { get; }
    IProductService ProductService { get; }
    ITokenService TokenService { get; }
    IUserService UserService { get; }
    ICartService CartService { get; }
}
