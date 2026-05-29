using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Commands.ChangeDisplayName;

public record ChangDisplayNameCommand(string? NewDisplayName) : IRequest<Result>;
