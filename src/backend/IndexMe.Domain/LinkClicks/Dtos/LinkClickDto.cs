using System.Diagnostics.CodeAnalysis;

namespace IndexMe.Domain.LinkClicks.Dtos;


public sealed class LinkClickDto
{
    [SetsRequiredMembers]
    private LinkClickDto(Guid id, DateTime clickedAt, string? ipAddress, string? userAgent)
    {
        Id = id;
        ClickedAt = clickedAt;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }

    public Guid Id { get; init; }
    public DateTime ClickedAt { get; init; }
    public string? IpAddress { get; init; }
    public string? UserAgent { get; init; }

    public static LinkClickDto Create(LinkClick linkClick)
    {
        return new LinkClickDto(
            id: linkClick.Id,
            clickedAt: linkClick.ClickedAt,
            ipAddress: linkClick.IpAddress,
            userAgent: linkClick.UserAgent
        );
    }
}

