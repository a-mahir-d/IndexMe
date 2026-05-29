using IndexMe.Application.Abstractions;
using IndexMe.Domain.Abstractions;
using IndexMe.Domain.Links;
using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Links.Commands.ChangeTitle;

public sealed class ChangeTitleCommandHandler(ILinkRepository linkRepository, ICurrentUserProvider currentUser, IUnitOfWork unitOfWork) : IRequestHandler<ChangeTitleCommand, Result>
{
    public async Task<Result> Handle(ChangeTitleCommand request, CancellationToken cancellationToken)
    {
        var link = await linkRepository.GetByIdAsync(request.LinkId, cancellationToken);
        if (link is null) return Result.Failure("LINK_NOT_FOUND");

        var userId = currentUser.UserId;
        if (link.UserId != userId) return Result.Failure("UNAUTHORIZED_ACCESS_ATTEMPT", $"UNAUTHORIZED_ACCESS_ATTEMPT | User(Id: {userId}) tried to change the title of the link(Id: {link.Id}) to {request.NewTitle}");

        link.ChangeTitle(request.NewTitle);
        linkRepository.Update(link);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
