using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Links.Commands.ChangeDisplayOrder;

public record ChangeDisplayOrderCommand(Guid LinkId, int NewDisplayOrder) : IRequest<Result>;
