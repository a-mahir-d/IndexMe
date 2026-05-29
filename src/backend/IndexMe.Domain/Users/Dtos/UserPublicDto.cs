using IndexMe.Domain.Links.Dtos;
using System.Diagnostics.CodeAnalysis;

namespace IndexMe.Domain.Users.Dtos;

public sealed class UserPublicDto
{
    [SetsRequiredMembers]
    private UserPublicDto(string userName, string? displayName, string? bio, ICollection<LinkPublicDto> userLinksPublicDtos)
    {
        Username = userName;
        DisplayName = displayName;
        Bio = bio;
        Links = userLinksPublicDtos;
    }

    public string Username { get; init; }
    public string? DisplayName { get; init; }
    public string? Bio { get; init; }
    public ICollection<LinkPublicDto> Links { get; init; }

    public static UserPublicDto Create(User user)
    {
        var userLinksPublicDtos = user.Links.Select(LinkPublicDto.Create).ToList();

        return new UserPublicDto(
            userName: user.Username.Value,
            displayName: user.DisplayName ?? user.Username.Value,
            bio: user.Bio,
            userLinksPublicDtos: userLinksPublicDtos
        );
    }
}
