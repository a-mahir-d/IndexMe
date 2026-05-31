using IndexMe.Application.Abstractions;
using IndexMe.Domain.Abstractions;
using IndexMe.Domain.Links;
using IndexMe.Domain.Results;
using IndexMe.Domain.Users;
using TS.MediatR;

namespace IndexMe.Application.Features.Links.Commands.CreateLink;

public sealed class CreateLinkCommandHandler(ILinkRepository linkRepository, ICurrentUserProvider currentUser, IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateLinkCommand, Result>
{
    private static int MaxLinkCountPerUser = 30;
    public async Task<Result> Handle(CreateLinkCommand request, CancellationToken cancellationToken)
    {
        var id = currentUser.UserId;
        var user = await userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null) return Result.Failure("USER_NOT_FOUND");

        var linkCountOfUser = await linkRepository.GetCountByUserIdAsync(user.Id, cancellationToken);
        if (linkCountOfUser >= 30) return Result.Failure($"USER_CANNOT_HAVE_MORE_THAN_{MaxLinkCountPerUser}_LINKS");

        var link = Link.Create(request.Title, request.Url, 0, user);
        await linkRepository.AddAsync(link, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
