using IndexMe.Domain.LinkClicks;

namespace IndexMe.Application.Analytics;

public interface IClickChannel
{
    ValueTask WriteAsync(LinkClick click, CancellationToken cancellationToken = default);
    IAsyncEnumerable<LinkClick> ReadAllAsync(CancellationToken cancellationToken = default);
}
