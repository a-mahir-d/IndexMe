namespace IndexMe.Application.Abstractions;

public interface IJwtService
{
    string GenerateToken(Guid id, string email, string userName);
}
