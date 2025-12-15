using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using CleanCode.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(IServiceFacade services, IMapper mapper) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromHeader(Name = "X-Auth-Token")] string token, CartDto dto, CancellationToken ct)
        {
            var user = await services.UserService.GetByTokenAsync(token, ct);
            if (user == null) return Unauthorized();
            
            dto.UserId = user.Id;
            dto.CreatedAt = DateTime.Now;
            
            await services.CartService.AddAsync(mapper.Map<Cart>(dto), ct);
            return Created();
        }
    }
}
