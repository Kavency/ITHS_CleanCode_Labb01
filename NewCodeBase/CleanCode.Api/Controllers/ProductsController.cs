using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using CleanCode.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService _productService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll(CancellationToken ct)
        {
            var products = await _productService.GetAllAsync(ct);
            return Ok(_mapper.Map<List<ProductDto>>(products));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto?>> GetById(int id, CancellationToken ct)
        {
            var product = await _productService.GetByIdAsync(id, ct);
            return product != null ? Ok(_mapper.Map<ProductDto>(product)) : NotFound();
        }


        [HttpPost]
        public async Task<ActionResult> Add(CreateProductDto dto, CancellationToken ct)
        {
            var product = _mapper.Map<Product>(dto);
            await _productService.AddAsync(product, ct);

            var productDto = _mapper.Map<ProductDto>(product);
            return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateProductDto dto, CancellationToken ct)
        {
            if (id != dto.Id) return BadRequest();
            
            var product = _mapper.Map<Product>(dto);
            await _productService.UpdateAsync(product, ct);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
        {
            var product = await _productService.GetByIdAsync(id, ct);
            if (product is null) return NotFound();

            await _productService.RemoveAsync(product, ct);
            return NoContent();
        }


        [HttpPost("{id}/stock/increase")]
        public async Task<IActionResult> IncreaseStock(int id, [FromQuery] int amount, CancellationToken ct)
        {           
            if (amount <= 0) return BadRequest("Amount must be > 0");
            var ok = await _productService.IncreaseStockAsync(id, amount, ct);
            if (!ok) return NotFound();
            return Ok();
        }


        [HttpPost("{id}/stock/decrease")]
        public async Task<IActionResult> DecreaseStock(int id, [FromQuery] int amount, CancellationToken ct)
        {           
            if (amount <= 0) return BadRequest("Amount must be > 0");
            var ok = await _productService.DecreaseStockAsync(id, amount, ct);
            if (!ok) return NotFound();
            return Ok();
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query, [FromQuery] decimal? maxPrice, CancellationToken ct)
        {            
            var result = await _productService.SearchAsync(query, maxPrice, ct);
            if (maxPrice.HasValue) result = result.Where(x => x.Price <= maxPrice.Value).ToList();
            return Ok(_mapper.Map<List<ProductDto>>(result));
        }
    }
}
