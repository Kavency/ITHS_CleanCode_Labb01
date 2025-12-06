namespace CleanCode.Application.Dtos;

public class UserDto
{
    public int Id { get; init; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
