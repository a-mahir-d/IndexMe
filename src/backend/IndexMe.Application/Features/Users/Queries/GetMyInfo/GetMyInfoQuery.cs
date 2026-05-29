using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Users.Queries.GetMyInfo;

public record GetMyInfoQuery() : IRequest<Result>;
