using System.ComponentModel.DataAnnotations;

namespace IndexMe.Infrastructure.Settings;

public class DbSettings
{
    [Required]
    public required string ConnectionString { get; set; }
}
