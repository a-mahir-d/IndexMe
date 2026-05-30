using IndexMe.Application.Services;
using TS.MediatR;

namespace IndexMe.Application.Behaviors;

public sealed class TrimAndChopStringsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public TrimAndChopStringsBehavior() : base() { }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TrimAndChopStringsService.TrimAndChop(request);
        var response = await next();
        return response;
    }
}
