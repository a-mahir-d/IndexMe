using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Queries.GetUserInfoByUsername;

public record GetUserInfoByUsernameQuery(string Username) : IRequest<Result>;
