using IndexMe.Domain.LinkClicks;
using IndexMe.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IndexMe.Infrastructure.Repositories;

public sealed class LinkClickRepository(IndexMeDbContext context) : ILinkClickRepository
{
    public async Task AddAsync(LinkClick linkClick, CancellationToken cancellationToken = default)
        => await context.LinkClicks.AddAsync(linkClick, cancellationToken);

    public async Task AddRangeAsync(IEnumerable<LinkClick> linkClicks, CancellationToken cancellationToken = default)
        => await context.LinkClicks.AddRangeAsync(linkClicks, cancellationToken);

    public async Task<int> GetClickCountByLinkIdAsync(Guid linkId, CancellationToken cancellationToken = default)
        => await context.LinkClicks.CountAsync(lc => lc.LinkId == linkId, cancellationToken);
}
