using CleanCode.Application.Interfaces;
using CleanCode.Core.Entities;

namespace CleanCode.Application.Services;

public class TokenService(IUnitOfWork unitOfWork) : ITokenService
{
    public async Task AddTokenAsync(UserToken userToken, CancellationToken ct)
    {
        await unitOfWork.UserTokens.AddAsync(userToken, ct);
        await unitOfWork.SaveChangesAsync(ct);
    }
}
