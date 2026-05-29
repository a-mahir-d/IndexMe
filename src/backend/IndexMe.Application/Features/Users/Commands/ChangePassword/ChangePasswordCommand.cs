using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Commands.ChangePassword;

public record ChangePasswordCommand(string NewPassword) : IRequest<Result>;
