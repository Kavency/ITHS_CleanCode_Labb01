using System.Threading.Tasks;
using CleanCode.Application.Interfaces;
using CleanCode.Core.Entities;
using CleanCode.Core.Interfaces.Services;

namespace CleanCode.Application.Services;

public class UserService(IUnitOfWork _unitOfWork) : IUserService
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

        return await _unitOfWork.Users.GetByIdAsync(userToken.UserId, ct);
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
}
