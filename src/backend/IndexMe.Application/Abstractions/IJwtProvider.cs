namespace IndexMe.Application.Abstractions;

public interface IJwtProvider
{
    string GenerateToken(Guid id, string email);
}
