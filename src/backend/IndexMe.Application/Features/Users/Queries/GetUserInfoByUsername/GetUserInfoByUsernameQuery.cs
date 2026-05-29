using IndexMe.Domain.Results;
using IndexMe.Domain.Users.Dtos;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Queries.GetUserInfoByUsername;

public record GetUserInfoByUsernameQuery(string Username) : IRequest<Result<UserPublicDto>>;
