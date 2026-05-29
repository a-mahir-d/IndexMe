using System.Diagnostics.CodeAnalysis;

namespace IndexMe.Domain.Links.Dtos;

public sealed class LinkPublicDto
{
    [SetsRequiredMembers]
    private LinkPublicDto(Guid id, string title, int displayOrder)
    {
        Id = id;
        Title = title;
        DisplayOrder = displayOrder;
    }

    public Guid Id { get; init; }
    public string Title { get; init; }
    public int DisplayOrder { get; init; }

    public static LinkPublicDto Create(Link link)
    {
        return new LinkPublicDto(
            id: link.Id,
            title: link.Title,
            displayOrder: link.DisplayOrder
        );
    }
}
