using IndexMe.Domain.Results;

namespace IndexMe.Application.Abstractions;

public interface IJwtService
{
    string GenerateToken(Guid id, string email, string userName);
    Task<Result> ValidateToken(string token);
}
