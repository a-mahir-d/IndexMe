namespace IndexMe.Domain.Links.Dtos;

public sealed record LinkClickCountDto
{
    public Guid LinkId { get; init; }
    public int ClickCount { get; init; }
}
