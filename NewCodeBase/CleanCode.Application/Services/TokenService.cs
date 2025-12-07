using CleanCode.Application.Interfaces;
using CleanCode.Core.Entities;

namespace CleanCode.Application.Services;

public class TokenService(IUnitOfWork _unitOfWork) : ITokenService
{
    public async Task AddTokenAsync(UserToken userToken, CancellationToken ct)
    {
        await _unitOfWork.UserTokens.AddAsync(userToken, ct);
        await _unitOfWork.SaveChangesAsync(ct);
    }


    public async Task<string> CreateAndSaveTokenAsync(int userId, CancellationToken ct)
    {
        var token = "token-" + Guid.NewGuid().ToString();
        await AddTokenAsync(new UserToken { Token = token, UserId = userId }, ct);
        return token;
    }
}
