using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Commands.LoginUser;

public record LoginUserCommand(string Email, string Password) : IRequest<Result>;
