using System.ComponentModel.DataAnnotations;

namespace API.Infrastructure.Models;

/// <summary>
/// User credentials.
/// </summary>
public record UserCredentials
{
    /// <summary>
    /// Email.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Login { get; init; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Password { get; init; }
}
