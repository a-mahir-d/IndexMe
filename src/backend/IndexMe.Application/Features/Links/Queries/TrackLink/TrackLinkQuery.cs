using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Links.Queries.TrackLink;

public record TrackClickQuery(Guid LinkId) : IRequest<Result<string>>;
