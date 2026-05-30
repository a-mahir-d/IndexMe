using IndexMe.Application.Abstractions;
using IndexMe.Domain.Links;
using IndexMe.Domain.Results;
using IndexMe.Domain.Users;
using IndexMe.Domain.Users.Dtos;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Queries.GetMyInfo;

public sealed class GetMyInfoQueryHandler(IUserRepository userRepository, ILinkRepository linkRepository, ICurrentUserProvider currentUser) : IRequestHandler<GetMyInfoQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetMyInfoQuery request, CancellationToken cancellationToken)
    {
        var id = currentUser.UserId;
        var user = await userRepository.GetByIdWithLinksAsync(id, cancellationToken);
        if (user is null) return Result<UserDto>.Failure("USER_NOT_FOUND");

        var clickCounts = await linkRepository.GetLinkClickCountsAsync(user.Id, cancellationToken);

        var userDto = UserDto.Create(user);

        foreach (var link in userDto.Links)
        {
            var count = clickCounts.FirstOrDefault(c => c.LinkId == link.Id)?.ClickCount ?? 0;
            link.ClickCount = count;
        }

        return Result<UserDto>.Success(userDto);
    }
}
