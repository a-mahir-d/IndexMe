using System.Text.Json.Serialization;

namespace IndexMe.Application.Models;

public class IpApiResponse
{
    [JsonPropertyName("countryCode")]
    public required string CountryCode { get; set; }
}
