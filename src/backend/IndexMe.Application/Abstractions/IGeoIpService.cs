namespace IndexMe.Application.Abstractions;

public interface IGeoIpService
{
    Task<string> GetCountryCodeAsync(string? ip, CancellationToken cancellationToken = default);
}
