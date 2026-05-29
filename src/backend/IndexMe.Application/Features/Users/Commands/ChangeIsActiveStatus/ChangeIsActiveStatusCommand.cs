using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Commands.ChangeIsActiveStatus;

public record ChangeIsActiveStatusCommand(bool NewIsActiveStatus) : IRequest<Result>;
