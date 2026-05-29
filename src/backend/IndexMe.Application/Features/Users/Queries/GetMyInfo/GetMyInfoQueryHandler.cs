using IndexMe.Domain.Results;
using IndexMe.Domain.Users;
using IndexMe.Domain.Users.Dtos;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Queries.GetMyInfo;

public sealed class GetMyInfoQueryHandler(IUserRepository userRepository) : IRequestHandler<GetMyInfoQuery, Result>
{
    public async Task<Result> Handle(GetMyInfoQuery request, CancellationToken cancellationToken)
    {
        // var id = getFromToken
        var id = Guid.CreateVersion7();
        var user = await userRepository.GetByIdWithLinksAsync(id, cancellationToken);
        if (user is null) return Result.Failure("USER_NOT_FOUND");

        var userDto = UserDto.Create(user);
        return Result<UserDto>.Success(userDto);
    }
}
