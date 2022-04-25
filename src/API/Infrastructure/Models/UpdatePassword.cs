using System.ComponentModel.DataAnnotations;

namespace API.Infrastructure.Models;

/// <summary>
/// Update password.
/// </summary>
public record UpdatePassword
{
    /// <summary>
    /// ID.
    /// </summary>
    [Required]
    public int? Id { get; init; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Password { get; init; }
}
