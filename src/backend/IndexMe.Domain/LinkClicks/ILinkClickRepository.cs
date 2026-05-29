namespace IndexMe.Domain.LinkClicks;

public interface ILinkClickRepository
{
    Task AddAsync(LinkClick linkClick, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<LinkClick> linkClicks, CancellationToken cancellationToken = default);

    // CV'de fark yaratacak bir analitik sorgu arayüzü örneği
    Task<int> GetClickCountByLinkIdAsync(Guid linkId, CancellationToken cancellationToken = default);
}
