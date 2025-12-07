using CleanCode.Core.Entities;

namespace CleanCode.Core.Interfaces;

public interface ITokenRepository
{
    Task AddAsync(UserToken token, CancellationToken ct);
    Task<UserToken?> GetIdByTokenAsync(string token, CancellationToken ct);
}
