using IndexMe.Application.Abstractions;
using IndexMe.Application.Models;
using System.Net.Http.Json;

namespace IndexMe.Infrastructure.Services;

public class GeoIpService(HttpClient httpClient) : IGeoIpService
{
    public async Task<string> GetCountryCodeAsync(string? ip, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(ip)) return "XX";

        try
        {
            var response = await httpClient.GetFromJsonAsync<IpApiResponse>($"http://ip-api.com/json/{ip}", cancellationToken);
            return response?.CountryCode ?? "XX";
        }
        catch
        {
            return "XX";
        }
    }
}
