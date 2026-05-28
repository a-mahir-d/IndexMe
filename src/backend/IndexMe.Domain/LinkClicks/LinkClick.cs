using IndexMe.Domain.Abstractions;
using IndexMe.Domain.Links;

namespace IndexMe.Domain.LinkClicks;

public sealed class LinkClick : Entity<Guid>
{
    public Guid LinkId { get; init; }
    public DateTime ClickedAt { get; init; }
    public string? IpAddress { get; init; }
    public string? UserAgent { get; init; }
    public Link Link { get; init; }
}
