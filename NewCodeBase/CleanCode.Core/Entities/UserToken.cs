namespace CleanCode.Core.Entities;

public class UserToken
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
