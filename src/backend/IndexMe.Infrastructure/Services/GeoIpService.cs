using IndexMe.Application.Abstractions;
using IndexMe.Application.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;

namespace IndexMe.Infrastructure.Services;

public class GeoIpService(HttpClient httpClient, IMemoryCache cache) : IGeoIpService
{
    public async Task<string> GetCountryCodeAsync(string? ip, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(ip)) return "XX";

        string cacheKey = $"geoip:{ip}";
        if (cache.TryGetValue(cacheKey, out string? cachedCountryCode) && cachedCountryCode != null)
        {
            return cachedCountryCode;
        }

        try
        {
            var response = await httpClient.GetFromJsonAsync<IpApiResponse>($"http://ip-api.com/json/{ip}", cancellationToken);
            var countryCode = response?.CountryCode ?? "XX";

            if (countryCode != "XX")
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1))
                    .SetSlidingExpiration(TimeSpan.FromHours(4));

                cache.Set(cacheKey, countryCode, cacheOptions);
            }

            return countryCode;
        }
        catch
        {
            return "XX";
        }
    }
}
