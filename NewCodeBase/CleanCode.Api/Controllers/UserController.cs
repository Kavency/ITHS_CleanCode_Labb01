using AutoMapper;
using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using CleanCode.Application.Models;
using CleanCode.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanCode.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IServiceFacade services, IMapper mapper) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] CreateUserDto dto, CancellationToken ct)
    {
        var created = await services.UserService.RegisterAsync(mapper.Map<User>(dto), ct);
        if (!created) return Conflict("User already exists.");

        return Ok(new { Message = "Registered" });
    }


    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest login, CancellationToken ct)
    {
        var response = await services.UserService.GetByUsernameAsync(login, ct);
        if (response is null) return Unauthorized("Invalid username or password.");
        return Ok(response);
    }


    [HttpGet("profile")]
    public async Task<ActionResult<UserProfileDto>> Profile([FromHeader(Name = "X-Auth-Token")] string token, CancellationToken ct)
    {
        var result = await services.UserService.GetByTokenAsync(token, ct);

        return result is null
            ? NotFound()
            : Ok(mapper.Map<UserProfileDto>(result));
    }


    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll(CancellationToken ct)
    {
        var result = await services.UserService.GetAllAsync(ct);
        return Ok(mapper.Map<List<UserDto>>(result));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(int id, CancellationToken ct)
    {
        var result = await services.UserService.GetByIdAsync(id, ct);

        return result is null
            ? NotFound()
            : Ok(mapper.Map<UserDto>(result));
    }
}

