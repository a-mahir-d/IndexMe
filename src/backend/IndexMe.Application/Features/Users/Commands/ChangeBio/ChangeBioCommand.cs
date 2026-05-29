using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Commands.ChangeBio;

public record ChangeBioCommand(string? NewBio) : IRequest<Result>;
