using IndexMe.Domain.Users;
using IndexMe.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IndexMe.Infrastructure.Repositories;

public sealed class UserRepository(IndexMeDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public async Task<User?> GetByUsernameAsync(Username username, CancellationToken cancellationToken = default)
        => await context.Users.FirstOrDefaultAsync(u => u.Username == username, cancellationToken);

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
        => await context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        => await context.Users.AddAsync(user, cancellationToken);

    public void Update(User user) => context.Users.Update(user);
    public void Delete(User user) => context.Users.Remove(user);
}
