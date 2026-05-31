using System.Diagnostics.CodeAnalysis;

namespace IndexMe.Domain.LinkClicks.Dtos;


public sealed class LinkClickDto
{
    [SetsRequiredMembers]
    private LinkClickDto(Guid id, DateTime clickedAt, string countryCode, string? userAgent)
    {
        Id = id;
        ClickedAt = clickedAt;
        CountryCode = countryCode;
        UserAgent = userAgent;
    }

    public Guid Id { get; init; }
    public DateTime ClickedAt { get; init; }
    public string CountryCode { get; init; }
    public string? UserAgent { get; init; }

    public static LinkClickDto Create(LinkClick linkClick, string countryCode)
    {
        return new LinkClickDto(
            id: linkClick.Id,
            clickedAt: linkClick.ClickedAt,
            countryCode: countryCode,
            userAgent: linkClick.UserAgent
        );
    }
}

