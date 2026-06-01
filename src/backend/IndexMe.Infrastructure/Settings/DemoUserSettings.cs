using System.ComponentModel.DataAnnotations;

namespace IndexMe.Infrastructure.Settings;

public class DemoUserSettings
{
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}