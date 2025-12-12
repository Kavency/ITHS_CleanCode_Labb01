using CleanCode.Core.Entities;

namespace CleanCode.Application.Interfaces;

public interface ITokenService
{
    Task AddTokenAsync(UserToken userToken, CancellationToken ct);
}
