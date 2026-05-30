using IndexMe.Domain.Abstractions;
using IndexMe.Domain.LinkClicks;
using IndexMe.Domain.Users;

namespace IndexMe.Domain.Links;

public sealed class Link : Entity<Guid>
{
#pragma warning disable CS8618
    [Obsolete("For EF Core use only", true)]
    private Link() { }
#pragma warning restore CS8618

    private Link(Guid id, Guid userId, Title title, Url url, byte displayOrder, DateTime createdAt, User user, ICollection<LinkClick> clicks)
    {
        Id = id;
        UserId = userId;
        Title = title;
        Url = url;
        DisplayOrder = displayOrder;
        CreatedAt = createdAt;
        User = user;
        Clicks = clicks;
    }

    public Guid UserId { get; init; }
    public Title Title { get; private set; }
    public Url Url { get; private set; }
    public byte DisplayOrder { get; private set; }
    public DateTime CreatedAt { get; init; }
    public User User { get; init; }
    public ICollection<LinkClick> Clicks { get; private set; }

    public static Link Create(string title, string url, byte displayOrder, User user)
    {
        return new Link(
            id: Guid.CreateVersion7(),
            userId: user.Id,
            title: new(title),
            url: new(url),
            displayOrder: displayOrder,
            createdAt: DateTime.UtcNow,
            user: user,
            clicks: []
        );
    }

    public void ChangeTitle(string title) => Title = new(title);
    public void ChangeUrl(string url) => Url = new(url);
    public void ChangeDisplayOrder(byte displayOrder) => DisplayOrder = displayOrder;
}
