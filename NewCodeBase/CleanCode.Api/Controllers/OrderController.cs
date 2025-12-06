using AutoMapper;
using CleanCode.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService _service, IMapper _mapper) : ControllerBase
    {
        [HttpGet("create")]
        public IActionResult GetOrder([FromHeader(Name = "X-Auth-Token")] string token)
        {
            return Ok();
        }


        [HttpPost("create")]
        public IActionResult CreateOrder([FromHeader(Name = "X-Auth-Token")] string token)
        {
            return Ok();
        }
    }
}
