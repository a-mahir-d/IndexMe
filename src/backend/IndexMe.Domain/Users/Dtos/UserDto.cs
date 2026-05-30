using IndexMe.Domain.Links.Dtos;
using System.Diagnostics.CodeAnalysis;

namespace IndexMe.Domain.Users.Dtos;

public sealed class UserDto
{
    [SetsRequiredMembers]
    private UserDto(string userName, string email, string? displayName, string? bio, DateTime createdAt, ICollection<LinkDto> links)
    {
        Username = userName;
        Email = email;
        DisplayName = displayName;
        Bio = bio;
        CreatedAt = createdAt;
        Links = links;
    }

    public Guid Id { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }
    public string? DisplayName { get; init; }
    public string? Bio { get; init; }
    public DateTime CreatedAt { get; init; }
    public ICollection<LinkDto> Links { get; init; }

    public static UserDto Create(User user)
    {
        return new UserDto(
            userName: user.Username.Value,
            email: user.Email.Value,
            displayName: user.DisplayName?.Value,
            bio: user.Bio?.Value,
            createdAt: user.CreatedAt,
            links: user.Links
        );
    }
}
