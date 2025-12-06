using CleanCode.Core.Entities;
using CleanCode.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleanCode.Infrastructure.Persistance;

public class UserRepository(AppDbContext _context) : IUserRepository
{
    public async Task<List<User>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Users.ToListAsync(ct);
    }
    
    
    public async Task<User?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id, ct);
    }


    public async Task<bool> CheckIfUserExistsAsync(string email, CancellationToken ct)
    {
        return await _context.Users.AnyAsync(u => u.Email == email, ct);
    }


    public void Add(User user) => _context.Users.Add(user);
    public void Remove(User user) => _context.Users.Remove(user);
    public void Update(User user) => _context.Users.Update(user);
}
