using IndexMe.Domain.Abstractions;
using IndexMe.Domain.Results;
using IndexMe.Domain.Users;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Commands.ChangeEmail;

public sealed class ChangeEmailCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<ChangeEmailCommand, Result>
{
    public async Task<Result> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        // var id = getFromToken
        var id = Guid.CreateVersion7();
        var user = await userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null) return Result.Failure("USER_NOT_FOUND");

        user.ChangeEmail(request.NewEmail);
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
