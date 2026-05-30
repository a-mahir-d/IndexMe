namespace IndexMe.Domain.Links;

public interface ILinkRepository
{
    Task<Link?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Link>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<byte> GetCountByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Link link, CancellationToken cancellationToken = default);
    void Update(Link link);
    void Delete(Link link);
}
