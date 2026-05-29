using IndexMe.Domain.Results;
using TS.MediatR;

namespace IndexMe.Application.Features.Links.Commands.CreateLink;

public record CreateLinkCommand(string Title, string Url) : IRequest<Result>;
