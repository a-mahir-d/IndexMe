using IndexMe.Domain.Links;
using IndexMe.Domain.Links.Dtos;
using IndexMe.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IndexMe.Infrastructure.Repositories;

public sealed class LinkRepository(IndexMeDbContext context) : ILinkRepository
{
    public async Task<Link?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Links.FirstOrDefaultAsync(l => l.Id == id, cancellationToken);

    public async Task<IEnumerable<Link>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        => await context.Links.Where(l => l.UserId == userId).ToListAsync(cancellationToken);

    public async Task<byte> GetCountByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        => (byte)await context.Links.Where(l => l.UserId == userId).CountAsync(cancellationToken);

    public async Task<List<LinkClickCountDto>> GetLinkClickCountsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.LinkClicks.Where(lc => lc.Link.UserId == userId).GroupBy(lc => lc.LinkId)
            .Select(g => new LinkClickCountDto
            {
                LinkId = g.Key,
                ClickCount = g.Count()
            })
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Link link, CancellationToken cancellationToken = default)
        => await context.Links.AddAsync(link, cancellationToken);

    public void Update(Link link) => context.Links.Update(link);

    public void Delete(Link link) => context.Links.Remove(link);
}
