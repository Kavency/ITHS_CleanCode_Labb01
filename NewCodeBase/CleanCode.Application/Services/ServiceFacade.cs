using CleanCode.Application.Interfaces;

namespace CleanCode.Application.Services;

public class ServiceFacade : IServiceFacade
{
    public IOrderService OrderService { get; }
    public IProductService ProductService { get; }
    public ITokenService TokenService { get; }
    public IUserService UserService { get; }
    public ICartService CartService { get; }

    public ServiceFacade(IOrderService orderService, 
        IProductService productService, 
        ITokenService tokenService, 
        IUserService userService, 
        ICartService cartService)
    {
        OrderService = orderService;
        ProductService = productService;
        TokenService = tokenService;
        UserService = userService;
        CartService = cartService;
    }
}
