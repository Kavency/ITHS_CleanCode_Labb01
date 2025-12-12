using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using CleanCode.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IServiceFacade services, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll(CancellationToken ct)
        {
            var products = await services.ProductService.GetAllAsync(ct);
            return Ok(mapper.Map<List<ProductDto>>(products));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto?>> GetById(int id, CancellationToken ct)
        {
            var product = await services.ProductService.GetByIdAsync(id, ct);
            return product != null ? Ok(mapper.Map<ProductDto>(product)) : NotFound();
        }


        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] CreateProductDto dto, CancellationToken ct)
        {
            var product = mapper.Map<Product>(dto);
            await services.ProductService.AddAsync(product, ct);

            var productDto = mapper.Map<ProductDto>(product);
            return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
        }


        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update(int id,[FromBody]  UpdateProductDto dto, CancellationToken ct)
        {
            if (id != dto.Id) return BadRequest();
            
            var product = mapper.Map<Product>(dto);
            await services.ProductService.UpdateAsync(product, ct);
            return NoContent();
        }


        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
        {
            var product = await services.ProductService.GetByIdAsync(id, ct);
            if (product is null) return NotFound();

            await services.ProductService.RemoveAsync(product, ct);
            return NoContent();
        }


        [HttpPost("stock/increase/{id}")]
        public async Task<IActionResult> IncreaseStock(int id, [FromQuery] int amount, CancellationToken ct)
        {           
            if (amount <= 0) return BadRequest("Amount must be > 0");
            var ok = await services.ProductService.IncreaseStockAsync(id, amount, ct);
            if (!ok) return NotFound();
            return Ok();
        }


        [HttpPost("stock/decrease/{id}")]
        public async Task<IActionResult> DecreaseStock(int id, [FromQuery] int amount, CancellationToken ct)
        {           
            if (amount <= 0) return BadRequest("Amount must be > 0");
            var ok = await services.ProductService.DecreaseStockAsync(id, amount, ct);
            if (!ok) return NotFound();
            return Ok();
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? query, [FromQuery] decimal? maxPrice, CancellationToken ct)
        {            
            var result = await services.ProductService.SearchAsync(query, maxPrice, ct);
            if (maxPrice.HasValue) result = result.Where(x => x.Price <= maxPrice.Value).ToList();
            return Ok(mapper.Map<List<ProductDto>>(result));
        }
    }
}
