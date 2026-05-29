using IndexMe.Application.Abstractions;
using Microsoft.IdentityModel.JsonWebTokens;

namespace IndexMe.WebAPI.Services;

public sealed class CurrentUserProvider(IHttpContextAccessor httpContextAccessor) : ICurrentUserProvider
{
    private HttpContext Context => httpContextAccessor.HttpContext ?? throw new InvalidOperationException("Http context asenkron iş parçacığı dışında kullanılamaz.");

    public Guid UserId => Guid.TryParse(Context.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, out var id) ? id : Guid.Empty;
    public string Email => Context.User.FindFirst(JwtRegisteredClaimNames.Email)?.Value ?? string.Empty;
    public string Username => Context.User.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value ?? string.Empty;
    public string? Ip => Context.Items["ClientIp"] as string;
    public string? UserAgent => Context.Items["UserAgent"] as string;
}
