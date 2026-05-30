using IndexMe.Application.Abstractions;
using IndexMe.Application.Analytics;
using IndexMe.Domain.LinkClicks;
using IndexMe.Domain.Links;
using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Links.Queries.TrackLink;

public sealed class TrackClickQueryHandler(ILinkRepository linkRepository, ICurrentUserProvider currentUser, IClickChannel clickChannel) : IRequestHandler<TrackClickQuery, Result<string>>
{
    public async Task<Result<string>> Handle(TrackClickQuery request, CancellationToken cancellationToken)
    {
        var link = await linkRepository.GetByIdAsync(request.LinkId, cancellationToken);
        if (link is null) return Result<string>.Failure("LINK_NOT_FOUND");

        var ipAddress = currentUser.Ip;
        var userAgent = currentUser.UserAgent;

        var click = LinkClick.Create(ipAddress, userAgent, link);

        await clickChannel.WriteAsync(click, cancellationToken);

        return Result<string>.Success(link.Url.Value);
    }
}
