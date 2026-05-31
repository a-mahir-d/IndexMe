using IndexMe.Domain.LinkClicks.Dtos;
using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.LinkClicks.Queries.GetClicksByLinkId;

public record GetClicksByLinkIdQuery(Guid LinkId) : IRequest<Result<List<LinkClickDto>>>;
