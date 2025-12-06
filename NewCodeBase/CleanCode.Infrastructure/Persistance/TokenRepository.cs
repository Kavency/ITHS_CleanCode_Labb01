using CleanCode.Core.Entities;
using CleanCode.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleanCode.Infrastructure.Persistance;

public class TokenRepository(AppDbContext _context) : ITokenRepository
{
    public async Task<UserToken?> GetIdByTokenAsync(string token, CancellationToken ct)
    {
        return await _context.UserTokens.FirstOrDefaultAsync(x => x.Token == token);
    }
}
