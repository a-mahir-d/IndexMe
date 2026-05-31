using IndexMe.Domain.Links.Dtos;

namespace IndexMe.Domain.Links;

public interface ILinkRepository
{
    Task<List<Link>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Link?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Link>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<byte> GetCountByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<List<LinkClickCountDto>> GetLinkClickCountsAsync(Guid userId, CancellationToken ct);
    Task AddAsync(Link link, CancellationToken cancellationToken = default);
    Task ExecuteShiftOrderAsync(Guid userId, int lowBound, int highBound, int shiftAmount, CancellationToken cancellationToken);
    void Update(Link link);
    void Delete(Link link);
}
