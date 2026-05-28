using IndexMe.Domain.Abstractions;
using IndexMe.Domain.Links;

namespace IndexMe.Domain.Users;

public sealed class User : Entity<Guid>
{

#pragma warning disable CS8618
    [Obsolete("For EF Core use only", true)]
    private User() { }
#pragma warning restore CS8618
    private User(Guid id, Username username, Email email, Password password, string? displayName, string? bio, DateTime createdAt, bool isActive, ICollection<Link> links)
    {
        Id = id;
        Username = username;
        Email = email;
        Password = password;
        DisplayName = displayName;
        Bio = bio;
        CreatedAt = createdAt;
        IsActive = isActive;
        Links = links;
    }

    public Username Username { get; init; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public string? DisplayName { get; private set; }
    public string? Bio { get; private set; }
    public DateTime CreatedAt { get; init; }
    public bool IsActive { get; private set; }
    public ICollection<Link> Links { get; private set; }

    public static User Create(string username, string email, string password, string? displayName, string? bio)
    {
        return new User(
            id: Guid.CreateVersion7(),
            username: new(username),
            email: new(email),
            password: new(password, true),
            displayName: displayName,
            bio: bio,
            createdAt: DateTime.UtcNow,
            isActive: true,
            links: []
        );
    }

    public void ChangeEmail(string email) => Email = new(email);
    public void ChangePassword(string password) => Password = new(password, true);
    public void ChangeDisplayName(string? displayName) => DisplayName = displayName;
    public void ChangeBio(string? bio) => Bio = bio;
    public void ChangeIsActiveStatus(bool isActive) => IsActive = isActive;
}
