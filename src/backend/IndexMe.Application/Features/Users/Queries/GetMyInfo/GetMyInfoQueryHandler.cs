using IndexMe.Application.Abstractions;
using IndexMe.Domain.Results;
using IndexMe.Domain.Users;
using IndexMe.Domain.Users.Dtos;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Queries.GetMyInfo;

public sealed class GetMyInfoQueryHandler(IUserRepository userRepository, ICurrentUserProvider currentUser) : IRequestHandler<GetMyInfoQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetMyInfoQuery request, CancellationToken cancellationToken)
    {
        var id = currentUser.UserId;
        var user = await userRepository.GetByIdWithLinksAsync(id, cancellationToken);
        if (user is null) return Result<UserDto>.Failure("USER_NOT_FOUND");

        var userDto = UserDto.Create(user);
        return Result<UserDto>.Success(userDto);
    }
}
