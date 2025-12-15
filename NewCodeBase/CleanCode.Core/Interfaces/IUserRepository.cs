using CleanCode.Core.Entities;

namespace CleanCode.Core.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync(CancellationToken ct);
    Task<User?> GetByIdAsync(int id, CancellationToken ct);
    Task<bool> CheckIfUserExistsAsync(string email, CancellationToken ct);
    Task<User?> GetByUsernameAsync(string userName, string password, CancellationToken ct);
    Task AddAsync(User user, CancellationToken ct);
    void Remove(User user);
    void Update(User user);
}
