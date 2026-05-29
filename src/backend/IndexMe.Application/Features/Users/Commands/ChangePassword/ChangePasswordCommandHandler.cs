using IndexMe.Application.Abstractions;
using IndexMe.Domain.Abstractions;
using IndexMe.Domain.Results;
using IndexMe.Domain.Users;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Commands.ChangePassword;

public sealed class ChangePasswordCommandHandler(IUserRepository userRepository, ICurrentUserProvider currentUser, IUnitOfWork unitOfWork) : IRequestHandler<ChangePasswordCommand, Result>
{
    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var id = currentUser.UserId;
        var user = await userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null) return Result.Failure("USER_NOT_FOUND");

        user.ChangePassword(request.NewPassword);
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
