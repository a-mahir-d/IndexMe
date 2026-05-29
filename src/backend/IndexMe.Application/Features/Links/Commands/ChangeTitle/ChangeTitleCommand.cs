using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Links.Commands.ChangeTitle;

public record ChangeTitleCommand(Guid LinkId, string NewTitle) : IRequest<Result>;
