using IndexMe.Domain.Results;
using IndexMe.Domain.Users.Dtos;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Queries.GetMyInfo;

public record GetMyInfoQuery() : IRequest<Result<UserDto>>;
