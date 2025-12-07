using CleanCode.Application.Interfaces;
using CleanCode.Application.Models;
using CleanCode.Core.Entities;
using CleanCode.Core.Interfaces;

namespace CleanCode.Application.Services;

public class UserService(IUnitOfWork _unitOfWork, ITokenService _tokenService) : IUserService
{
    public async Task<List<User>> GetAllAsync(CancellationToken ct)
    {
        return await _unitOfWork.Users.GetAllAsync(ct);
    }


    public async Task<User?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _unitOfWork.Users.GetByIdAsync(id, ct);
    }


    public async Task<User?> GetByTokenAsync(string token, CancellationToken ct)
    {
        var userToken = await _unitOfWork.UserTokens.GetIdByTokenAsync(token, ct);

        if (userToken is null)
            return null;

        return await GetByIdAsync(userToken.UserId, ct);
    }


    public async Task<bool> RegisterAsync(User user, CancellationToken ct)
    {
        var exists = await _unitOfWork.Users.CheckIfUserExistsAsync(user.Email.Trim().ToLowerInvariant(), ct);

        if (exists)
            return false;

        _unitOfWork.Users.Add(user);
        await _unitOfWork.SaveChangesAsync(ct);
        return true;
    }


    public async Task<LoginResponse> GetByUsernameAsync(LoginRequest login, CancellationToken ct)
    {
        var user = await _unitOfWork.Users.GetByUsernameAsync(login.Username, login.Password, ct);
        if (user is null) return null!;
        
        var token = await _tokenService.CreateAndSaveTokenAsync(user.Id, ct);
        return new LoginResponse { Token = token };
    }
}
