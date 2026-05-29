using IndexMe.Application.Abstractions;
using IndexMe.Domain.Results;
using IndexMe.Domain.Users;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Commands.LoginUser;

public sealed class LoginUserCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider) : IRequestHandler<LoginUserCommand, Result>
{
    public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);
        if (user is null) return Result.Failure("INVALID_CREDENTIALS");
        if (!user.Password.Verify(request.Password)) return Result.Failure("INVALID_CREDENTIALS");


        var token = jwtProvider.GenerateToken(user.Id, user.Email.Value);
        return Result<string>.Success(token);
    }
}
