using IndexMe.Domain.Abstractions;
using IndexMe.Domain.Links;

namespace IndexMe.Domain.LinkClicks;

public sealed class LinkClick : Entity<Guid>
{
#pragma warning disable CS8618
    [Obsolete("For EF Core use only", true)]
    private LinkClick() { }
#pragma warning restore CS8618

    private LinkClick(Guid id, Guid linkId, DateTime clickedAt, string? ipAddress, string? userAgent, Link link)
    {
        Id = id;
        LinkId = linkId;
        ClickedAt = clickedAt;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        Link = link;
    }

    public Guid LinkId { get; init; }
    public DateTime ClickedAt { get; init; }
    public string? IpAddress { get; init; }
    public string? UserAgent { get; init; }
    public Link Link { get; init; }

    public static LinkClick Create(Guid linkId, string? ipAddress, string? userAgent)
    {
        return new LinkClick(
            id: Guid.CreateVersion7(),
            linkId: linkId,
            clickedAt: DateTime.UtcNow,
            ipAddress: ipAddress,
            userAgent: userAgent,
            link: null!
        );
    }
}
