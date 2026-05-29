using IndexMe.Application.Analytics;
using IndexMe.Domain.LinkClicks;
using IndexMe.Domain.Links;
using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Links.Commands.TrackClick;

public sealed class TrackClickCommandHandler(ILinkRepository linkRepository, IClickChannel clickChannel) : IRequestHandler<TrackClickCommand, Result>
{
    public async Task<Result> Handle(TrackClickCommand request, CancellationToken cancellationToken)
    {
        var link = await linkRepository.GetByIdAsync(request.LinkId, cancellationToken);
        if (link is null) return Result.Failure("LINK_NOT_FOUND");

        var ipAddress = "dummy"; // get from user context
        var userAgent = "dummy"; // get from user context

        var click = LinkClick.Create(linkId: link.Id, ipAddress: ipAddress, userAgent: userAgent, link: link);

        await clickChannel.WriteAsync(click, cancellationToken);

        return Result<string>.Success(link.Url.Value);
    }
}
