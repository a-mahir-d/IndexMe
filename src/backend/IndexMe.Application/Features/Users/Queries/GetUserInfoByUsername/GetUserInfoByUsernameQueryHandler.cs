using IndexMe.Domain.Results;
using IndexMe.Domain.Users;
using IndexMe.Domain.Users.Dtos;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Queries.GetUserInfoByUsername;


public sealed class GetUserInfoByUsernameQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserInfoByUsernameQuery, Result<UserPublicDto>>
{
    public async Task<Result<UserPublicDto>> Handle(GetUserInfoByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsernameWithLinksAsync(new Username(request.Username), cancellationToken);
        if (user is null) return Result<UserPublicDto>.Failure("USER_NOT_FOUND");

        var userPublicDto = UserPublicDto.Create(user);
        return Result<UserPublicDto>.Success(userPublicDto);
    }
}
