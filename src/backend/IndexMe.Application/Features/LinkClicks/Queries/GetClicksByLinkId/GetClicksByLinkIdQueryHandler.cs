using IndexMe.Application.Abstractions;
using IndexMe.Domain.LinkClicks.Dtos;
using IndexMe.Domain.Links;
using IndexMe.Domain.Results;
using IndexMe.Domain.Users;
using TS.MediatR;

namespace IndexMe.Application.Features.LinkClicks.Queries.GetClicksByLinkId;

public sealed class GetClicksByLinkIdQueryHandler(ILinkRepository linkRepository, IGeoIpService geoApiService, IUserRepository userRepository, ICurrentUserProvider currentUser) : IRequestHandler<GetClicksByLinkIdQuery, Result<List<LinkClickDto>>>
{
    public async Task<Result<List<LinkClickDto>>> Handle(GetClicksByLinkIdQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId;
        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null) return Result<List<LinkClickDto>>.Failure("USER_NOT_FOUND");

        var link = await linkRepository.GetByIdWithClicksAsync(request.LinkId, cancellationToken);
        if (link is null) return Result<List<LinkClickDto>>.Failure("LINK_NOT_FOUND");

        if (link.UserId != userId) return Result<List<LinkClickDto>>.Failure("UNAUTHORIZED_ACCESS_ATTEMPT", $"UNAUTHORIZED_ACCESS_ATTEMPT | User(Id: {userId}) tried to get the link clicks(Link Id: {link.Id}) information of another user");

        List<LinkClickDto> linkClickDtos = [];
        foreach (var linkClick in link.Clicks)
        {
            var countryCode = await geoApiService.GetCountryCodeAsync(linkClick.IpAddress, cancellationToken);
            linkClickDtos.Add(LinkClickDto.Create(linkClick, countryCode));
        }

        return Result<List<LinkClickDto>>.Success(linkClickDtos);
    }
}