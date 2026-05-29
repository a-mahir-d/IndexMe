namespace IndexMe.Application.Abstractions;

public interface ICurrentUserProvider
{
    Guid UserId { get; }
    string Email { get; }
    string Username { get; }
    string? Ip { get; }
    string? UserAgent { get; }
}
