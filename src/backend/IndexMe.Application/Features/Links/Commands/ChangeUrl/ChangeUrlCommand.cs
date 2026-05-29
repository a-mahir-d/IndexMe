using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Links.Commands.ChangeUrl;

public record ChangeUrlCommand(Guid LinkId, string NewUrl) : IRequest<Result>;
