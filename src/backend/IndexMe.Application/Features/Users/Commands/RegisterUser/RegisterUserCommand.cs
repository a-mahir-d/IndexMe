using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Commands.RegisterUser;

public record RegisterUserCommand(string Username, string Email, string Password, string? DisplayName, string? Bio) : IRequest<Result>;