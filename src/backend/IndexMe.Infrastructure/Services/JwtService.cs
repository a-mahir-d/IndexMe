using IndexMe.Application.Abstractions;
using IndexMe.Infrastructure.Helpers;
using IndexMe.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace IndexMe.Infrastructure.Services;

internal sealed class JwtService(IOptions<JwtSettings> options) : IJwtService
{
    private readonly JwtSettings _settings = options.Value;
    private readonly RsaSecurityKey _signingKey = RsaKeyLoader.LoadPrivateKey(options.Value.PrivateKeyPath);

    public string GenerateToken(Guid userId, string email, string userName)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.UniqueName, userName)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _settings.Issuer,
            Audience = _settings.Audience,
            Expires = DateTime.UtcNow.AddMinutes(_settings.ExpiryInMinutes > 0 ? _settings.ExpiryInMinutes : 60),
            SigningCredentials = new SigningCredentials(
                _signingKey,
                SecurityAlgorithms.RsaSha256
            )
        };

        var tokenHandler = new JsonWebTokenHandler();
        return tokenHandler.CreateToken(tokenDescriptor);
    }
}
