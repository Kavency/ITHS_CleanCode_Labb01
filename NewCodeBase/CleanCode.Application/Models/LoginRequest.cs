using System.ComponentModel.DataAnnotations;

namespace CleanCode.Application.Models;

public class LoginRequest
{
    [Required]
    public string Username { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}
