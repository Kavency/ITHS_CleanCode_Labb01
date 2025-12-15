using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using CleanCode.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IServiceFacade services, IMapper mapper) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<OrderResponse>> CreateOrder([FromHeader(Name = "X-Auth-Token")] string token, CancellationToken ct)
        {
            var user = await services.UserService.GetByTokenAsync(token, ct);
            if (user == null) return Unauthorized();

            var result = await services.OrderService.CreateOrderForUserAsync(user.Id, ct);
            return Ok(result);
        }
        
        
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id, [FromHeader(Name = "X-Auth-Token")] string token, CancellationToken ct)
        {
            var user = await services.UserService.GetByTokenAsync(token, ct);
            if (user == null) return Unauthorized();
            
            var order = await services.OrderService.GetByIdAsync(id, ct);
            if (order == null || order.UserId != user.Id) return NotFound();

            return Ok(mapper.Map<OrderDto>(order));
        }
    }
}
