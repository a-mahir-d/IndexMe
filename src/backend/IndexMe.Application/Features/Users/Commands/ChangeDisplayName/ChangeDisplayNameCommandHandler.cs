using IndexMe.Application.Abstractions;
using IndexMe.Domain.Abstractions;
using IndexMe.Domain.Results;
using IndexMe.Domain.Users;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Commands.ChangeDisplayName;

public sealed class ChangeDisplayNameCommandHandler(IUserRepository userRepository, ICurrentUserProvider currentUser, IUnitOfWork unitOfWork) : IRequestHandler<ChangeDisplayNameCommand, Result>
{
    public async Task<Result> Handle(ChangeDisplayNameCommand request, CancellationToken cancellationToken)
    {
        var id = currentUser.UserId;
        var user = await userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null) return Result.Failure("USER_NOT_FOUND");

        user.ChangeDisplayName(request.NewDisplayName);
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
