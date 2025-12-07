using System.ComponentModel.DataAnnotations;

namespace CleanCode.Application.Dtos;

public class CreateUserDto
{
    [Required]
    public string Username { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";

    [Required]
    public string Email { get; set; } = "";
}
