using IndexMe.Application.Analytics;
using IndexMe.Domain.LinkClicks;
using System.Threading.Channels;

namespace IndexMe.Infrastructure.Analytics;

public sealed class ClickChannel : IClickChannel
{
    private readonly Channel<LinkClick> _channel = Channel.CreateBounded<LinkClick>(new BoundedChannelOptions(10000)
    {
        FullMode = BoundedChannelFullMode.Wait,
        SingleReader = true,
        SingleWriter = false
    });

    public ValueTask WriteAsync(LinkClick click, CancellationToken cancellationToken = default)
        => _channel.Writer.WriteAsync(click, cancellationToken);

    public IAsyncEnumerable<LinkClick> ReadAllAsync(CancellationToken cancellationToken = default)
        => _channel.Reader.ReadAllAsync(cancellationToken);
}
