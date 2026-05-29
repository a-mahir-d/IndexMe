using System.ComponentModel.DataAnnotations;

namespace IndexMe.Infrastructure.Settings;

public class ClientSettings
{
    [Required]
    public required string BaseUrl { get; set; }
}
