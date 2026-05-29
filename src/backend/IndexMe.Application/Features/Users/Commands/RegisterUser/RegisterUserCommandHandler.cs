using IndexMe.Domain.Abstractions;
using IndexMe.Domain.Results;
using IndexMe.Domain.Users;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<RegisterUserCommand, Result>
{
    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);
        if (existingUser is not null) return Result.Failure("EMAIL_IN_USE");

        existingUser = await userRepository.GetByUsernameAsync(new Username(request.Username), cancellationToken);
        if (existingUser is not null) return Result.Failure("USERNAME_TAKEN");

        var user = User.Create(request.Username, request.Email, request.Password, request.DisplayName, request.Bio);

        await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
