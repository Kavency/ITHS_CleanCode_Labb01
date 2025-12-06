using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService _service, IMapper _mapper) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] CreateUserDto dto, CancellationToken ct)
    {
        return Ok();
    }


    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserDto dto, CancellationToken ct)
    {
        return Ok();
    }


    [HttpGet("profile")]
    public async Task<ActionResult<UserProfileDto>> Profile([FromHeader(Name = "X-Auth-Token")] string token, CancellationToken ct)
    {
        var result = await _service.GetByTokenAsync(token, ct);
        
        return result is null
            ? NotFound()
            : Ok(_mapper.Map<UserProfileDto>(result));
    }


    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll(CancellationToken ct)
    {
        var result = await _service.GetAllAsync(ct);
        return Ok(_mapper.Map<List<UserDto>>(result));
    }


    [HttpGet]
    public async Task<ActionResult<UserDto>> GetById(int id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);

        return result is null
            ? NotFound()
            : Ok(_mapper.Map<UserDto>(result));
    }
}

