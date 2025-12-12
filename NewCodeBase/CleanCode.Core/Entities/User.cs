namespace CleanCode.Core.Entities;

public class User
{
    public int Id { get; init; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
