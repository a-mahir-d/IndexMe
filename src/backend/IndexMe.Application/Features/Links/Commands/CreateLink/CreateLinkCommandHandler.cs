using IndexMe.Application.Abstractions;
using IndexMe.Domain.Abstractions;
using IndexMe.Domain.Results;
using IndexMe.Domain.Users;
using TS.MediatR;

namespace IndexMe.Application.Features.Links.Commands.CreateLink;

public sealed class CreateLinkCommandHandler(ICurrentUserProvider currentUser, IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateLinkCommand, Result>
{
    public async Task<Result> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
    {
        var id = currentUser.UserId;
        var user = await userRepository.GetByIdWithLinksAsync(id, cancellationToken);
        if (user is null) return Result.Failure("USER_NOT_FOUND");

        user.AddLink(request.Title, request.Url);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
