namespace IndexMe.Domain.LinkClicks;

public interface ILinkClickRepository
{
    Task AddAsync(LinkClick linkClick, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<LinkClick> linkClicks, CancellationToken cancellationToken = default);
    Task<int> GetClickCountByLinkIdAsync(Guid linkId, CancellationToken cancellationToken = default);
}
