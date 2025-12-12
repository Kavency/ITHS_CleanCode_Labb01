using CleanCode.Application.Models;
using CleanCode.Core.Entities;

namespace CleanCode.Application.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllAsync(CancellationToken ct);
    Task<User?> GetByIdAsync(int id, CancellationToken ct);
    Task<User?> GetByTokenAsync(string token, CancellationToken ct);
    Task<bool> RegisterAsync(User user, CancellationToken ct);
    Task<LoginResponse> GetByUsernameAsync(LoginRequest login, CancellationToken ct);
}
