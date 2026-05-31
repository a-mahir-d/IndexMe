using IndexMe.Domain.Abstractions;
using IndexMe.Domain.Links;

namespace IndexMe.Domain.Users;

public sealed class User : Entity<Guid>
{
    private const int MaxLinkCount = 30;

#pragma warning disable CS8618
    [Obsolete("For EF Core use only", true)]
    private User() { }
#pragma warning restore CS8618
    private User(Guid id, Username username, Email email, Password password, DisplayName? displayName, Bio? bio, DateTime createdAt, ICollection<Link> links)
    {
        Id = id;
        Username = username;
        Email = email;
        Password = password;
        DisplayName = displayName;
        Bio = bio;
        CreatedAt = createdAt;
        Links = links;
    }

    public Username Username { get; init; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public DisplayName? DisplayName { get; private set; }
    public Bio? Bio { get; private set; }
    public DateTime CreatedAt { get; init; }
    public ICollection<Link> Links { get; private set; }

    public static User Create(string username, string email, string password, string? displayName, string? bio)
    {
        return new User(
            id: Guid.CreateVersion7(),
            username: new(username),
            email: new(email),
            password: new(password, true),
            displayName: new(displayName),
            bio: new(bio),
            createdAt: DateTime.UtcNow,
            links: []
        );
    }

    public void ChangeEmail(string email) => Email = new(email);
    public void ChangePassword(string password) => Password = new(password, true);
    public void ChangeDisplayName(string? displayName) => DisplayName = new(displayName);
    public void ChangeBio(string? bio) => Bio = new(bio);
}