using CleanCode.Core.Entities;
using CleanCode.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleanCode.Infrastructure.Persistance;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<List<User>> GetAllAsync(CancellationToken ct)
    {
        return await context.Users.ToListAsync(ct);
    }
    
    
    public async Task<User?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id, ct);
    }


    public async Task<User?> GetByUsernameAsync(string userName, string password, CancellationToken ct)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username == userName, ct);

        if (user is not null && user.Password == password) return user;
        
        return null;
    }
    
    
    public async Task<bool> CheckIfUserExistsAsync(string email, CancellationToken ct)
    {
        return await context.Users.AnyAsync(u => u.Email == email, ct);
    }


    public async Task AddAsync(User user, CancellationToken ct) => await context.Users.AddAsync(user, ct);
    public void Update(User user) => context.Users.Update(user);
    public void Remove(User user) => context.Users.Remove(user);
}
