using CleanCode.Core.Entities;

namespace CleanCode.Core.Interfaces;

public interface ITokenRepository
{
    Task<UserToken?> GetIdByTokenAsync(string token, CancellationToken ct);
}
