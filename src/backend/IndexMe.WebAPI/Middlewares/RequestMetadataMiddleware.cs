using IndexMe.WebAPI.Helpers;

namespace IndexMe.WebAPI.Middlewares;

public sealed class RequestMetadataMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var clientIp = ClientIpResolver.GetNormalizedClientIp(context);
        if (!string.IsNullOrWhiteSpace(clientIp))
        {
            context.Items["ClientIp"] = clientIp;
        }

        if (context.Request.Headers.TryGetValue("User-Agent", out var userAgent) && !string.IsNullOrWhiteSpace(userAgent))
        {
            context.Items["UserAgent"] = userAgent.ToString();
        }
        else
        {
            context.Items["UserAgent"] = "Unknown / Bot";
        }

        await next(context);
    }
}
