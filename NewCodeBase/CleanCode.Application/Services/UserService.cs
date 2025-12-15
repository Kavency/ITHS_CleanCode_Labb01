using CleanCode.Application.Interfaces;
using CleanCode.Application.Models;
using CleanCode.Core.Entities;

namespace CleanCode.Application.Services;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    public async Task<List<User>> GetAllAsync(CancellationToken ct)
    {
        return await unitOfWork.Users.GetAllAsync(ct);
    }


    public async Task<User?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await unitOfWork.Users.GetByIdAsync(id, ct);
    }


    public async Task<User?> GetByTokenAsync(string token, CancellationToken ct)
    {
        var userToken = await unitOfWork.UserTokens.GetIdByTokenAsync(token, ct);

        if (userToken is null)
            return null;

        return await GetByIdAsync(userToken.UserId, ct);
    }


    public async Task<bool> RegisterAsync(User user, CancellationToken ct)
    {
        var exists = await unitOfWork.Users.CheckIfUserExistsAsync(user.Email.Trim().ToLowerInvariant(), ct);

        if (exists)
            return false;

        await unitOfWork.Users.AddAsync(user, ct);
        await unitOfWork.SaveChangesAsync(ct);
        return true;
    }


    public async Task<LoginResponse> GetByUsernameAsync(LoginRequest login, CancellationToken ct)
    {
        var user = await unitOfWork.Users.GetByUsernameAsync(login.Username, login.Password, ct);
        if (user is null) return null!;
        
        var token = await CreateAndSaveTokenAsync(user.Id, ct);
        return new LoginResponse { Token = token };
    }


    private async Task<string> CreateAndSaveTokenAsync(int userId, CancellationToken ct)
    {
        var token = "token-" + Guid.NewGuid().ToString();
        await unitOfWork.UserTokens.AddAsync(new UserToken { UserId = userId, Token = token, CreatedAt = DateTime.Now }, ct);
        return token;
    }
}
