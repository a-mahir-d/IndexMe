using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Commands.ChangeEmail;

public record ChangeEmailCommand(string NewEmail) : IRequest<Result>;
