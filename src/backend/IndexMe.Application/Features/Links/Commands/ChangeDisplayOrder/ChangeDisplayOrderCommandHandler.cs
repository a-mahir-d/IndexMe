using IndexMe.Domain.Abstractions;
using IndexMe.Domain.Links;
using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Links.Commands.ChangeDisplayOrder;

public sealed class ChangeDisplayOrderCommandHandler(ILinkRepository linkRepository, IUnitOfWork unitOfWork) : IRequestHandler<ChangeDisplayOrderCommand, Result>
{
    public async Task<Result> Handle(ChangeDisplayOrderCommand request, CancellationToken cancellationToken)
    {
        var link = await linkRepository.GetByIdAsync(request.LinkId, cancellationToken);
        if (link is null) return Result.Failure("LINK_NOT_FOUND");

        var userId = Guid.CreateVersion7(); // get from token
        if (link.UserId != userId) return Result.Failure("UNAUTHORIZED_ACCESS_ATTEMPT", $"UNAUTHORIZED_ACCESS_ATTEMPT | User(Id: {userId}) tried to change the display order of the link(Id: {link.Id}) to {request.NewDisplayOrder}");


        // get all links
        // update the order

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
