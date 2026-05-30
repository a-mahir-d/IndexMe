using System.Diagnostics.CodeAnalysis;

namespace IndexMe.Domain.Links.Dtos;

public sealed class LinkDto
{
    [SetsRequiredMembers]
    private LinkDto(Guid id, string title, string url, int displayOrder, DateTime createdAt, int clickCount)
    {
        Id = id;
        Title = title;
        Url = url;
        DisplayOrder = displayOrder;
        CreatedAt = createdAt;
        ClickCount = clickCount;
    }

    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Url { get; init; }
    public int DisplayOrder { get; init; }
    public DateTime CreatedAt { get; init; }
    public int ClickCount { get; set; }

    public static LinkDto Create(Link link)
    {
        return new LinkDto(
            id: link.Id,
            title: link.Title.Value,
            url: link.Url.Value,
            displayOrder: link.DisplayOrder,
            createdAt: link.CreatedAt,
            clickCount: 0
        );
    }
}
