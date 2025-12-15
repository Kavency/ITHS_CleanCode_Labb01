namespace CleanCode.Application.Dtos;

public class UserProfileDto
{
    public int Id { get; init; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
