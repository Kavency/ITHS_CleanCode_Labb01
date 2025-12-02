using System.Threading.Tasks;
using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using CleanCode.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService _service, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(_mapper.Map<List<ProductDto>>(products));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto?>> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            return product != null ? Ok(_mapper.Map<ProductDto>(product)) : NotFound();
        }


        [HttpPost]
        public async Task<ActionResult> Add(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _service.AddAsync(product);

            var productDto = _mapper.Map<ProductDto>(product);
            return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateProductDto dto)
        {
            if (id != dto.Id) return BadRequest();
            
            var product = _mapper.Map<Product>(dto);
            await _service.UpdateAsync(product);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product is null) return NotFound();

            await _service.RemoveAsync(product);
            return NoContent();
        }


        [HttpPost("{id}/stock/increase")]
        public async Task<IActionResult> IncreaseStock(int id, [FromQuery] int amount)
        {           
            if (amount <= 0) return BadRequest("Amount must be > 0");
            var ok = await _service.IncreaseStockAsync(id, amount);
            if (!ok) return NotFound();
            return Ok();
        }


        [HttpPost("{id}/stock/decrease")]
        public async Task<IActionResult> DecreaseStock(int id, [FromQuery] int amount)
        {           
            if (amount <= 0) return BadRequest("Amount must be > 0");
            var ok = await _service.DecreaseStockAsync(id, amount);
            if (!ok) return NotFound();
            return Ok();
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query, [FromQuery] decimal? maxPrice)
        {            
            var result = await _service.SearchAsync(query, maxPrice);
            if (maxPrice.HasValue) result = result.Where(x => x.Price <= maxPrice.Value).ToList();
            return Ok(_mapper.Map<List<ProductDto>>(result));
        }
    }
}
