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

    private Link(Guid id, Guid userId, string title, Url url, int displayOrder, DateTime createdAt, User user, ICollection<LinkClick> clicks)
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
    public string Title { get; private set; }
    public Url Url { get; private set; }
    public int DisplayOrder { get; private set; }
    public DateTime CreatedAt { get; init; }
    public User User { get; init; }
    public ICollection<LinkClick> Clicks { get; private set; }

    public static Link Create(Guid userId, string title, string url, int displayOrder, User user)
    {
        return new Link(
            id: Guid.CreateVersion7(),
            userId: userId,
            title: title,
            url: new(url),
            displayOrder: displayOrder,
            createdAt: DateTime.UtcNow,
            user: user,
            clicks: []
        );
    }

    public void ChangeTitle(string title) => Title = title;
    public void ChangeUrl(string url) => Url = new(url);
    public void ChangeDisplayOrder(int displayOrder) => DisplayOrder = displayOrder;
}
