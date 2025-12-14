using CleanCode.Infrastructure.Persistance;
using CleanCode.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CleanCode.Tests.Persistance
{
    public class UserRepositoryTests
    {
        private static AppDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsSeededUsers()
        {
            using var context = CreateContext("GetAllUsersDb");
            var repo = new UserRepository(context);

            context.Users.Add(new User { Username = "a", Password = "p", Email = "a@a.com" });
            context.Users.Add(new User { Username = "b", Password = "p", Email = "b@a.com" });
            await context.SaveChangesAsync(default);

            var list = await repo.GetAllAsync(default);

            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectUserOrNull()
        {
            using var context = CreateContext("GetByIdDb");
            var repo = new UserRepository(context);

            var user = new User { Username = "u1", Password = "pw", Email = "u1@x.com" };
            context.Users.Add(user);
            await context.SaveChangesAsync(default);

            var fromDb = await repo.GetByIdAsync(user.Id, default);
            Assert.NotNull(fromDb);
            Assert.Equal(user.Email, fromDb!.Email);

            var notFound = await repo.GetByIdAsync(9999, default);
            Assert.Null(notFound);
        }

        [Fact]
        public async Task GetByUsernameAsync_ReturnsUserWhenPasswordMatches()
        {
            using var context = CreateContext("GetByUsernameDb");
            var repo = new UserRepository(context);

            var user = new User { Username = "john", Password = "secret", Email = "john@x.com" };
            context.Users.Add(user);
            await context.SaveChangesAsync(default);

            var ok = await repo.GetByUsernameAsync("john", "secret", default);
            Assert.NotNull(ok);
            Assert.Equal(user.Email, ok!.Email);

            var wrongPw = await repo.GetByUsernameAsync("john", "bad", default);
            Assert.Null(wrongPw);

            var missing = await repo.GetByUsernameAsync("missing", "any", default);
            Assert.Null(missing);
        }


        [Fact]
        public async Task AddUpdateRemove_ModifiesContext()
        {
            using var context = CreateContext("AddUpdRemDb");
            var repo = new UserRepository(context);

            var user = new User { Username = "temp", Password = "p", Email = "t@x.com" };
            await repo.AddAsync(user, default);
            await context.SaveChangesAsync(default);

            var fromDb = await repo.GetByIdAsync(user.Id, default);
            Assert.NotNull(fromDb);

            user.Email = "updated@x.com";
            repo.Update(user);
            await context.SaveChangesAsync(default);

            var updated = await repo.GetByIdAsync(user.Id, default);
            Assert.Equal("updated@x.com", updated!.Email);

            repo.Remove(user);
            await context.SaveChangesAsync(default);

            var removed = await repo.GetByIdAsync(user.Id, default);
            Assert.Null(removed);
        }
    }
}
