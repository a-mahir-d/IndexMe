using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Links.Commands.TrackClick;

public record TrackClickCommand(Guid LinkId) : IRequest<Result>;
