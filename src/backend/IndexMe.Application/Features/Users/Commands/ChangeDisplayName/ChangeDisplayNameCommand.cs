using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Commands.ChangeDisplayName;

public record ChangeDisplayNameCommand(string? NewDisplayName) : IRequest<Result>;
