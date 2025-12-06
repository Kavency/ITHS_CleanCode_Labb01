using CleanCode.Core.Entities;

namespace CleanCode.Core.Interfaces.Services;

public interface IUserService
{
    Task<List<User>> GetAllAsync(CancellationToken ct);
    Task<User?> GetByIdAsync(int id, CancellationToken ct);
    Task<User?> GetByTokenAsync(string token, CancellationToken ct);
    Task<bool> RegisterAsync(User user, CancellationToken ct);
}
