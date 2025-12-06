namespace CleanCode.Core.Entities;

public class UserToken
{
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
