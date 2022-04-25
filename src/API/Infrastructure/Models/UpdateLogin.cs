using System.ComponentModel.DataAnnotations;

namespace API.Infrastructure.Models;

/// <summary>
/// Update login.
/// </summary>
public record UpdateLogin
{
    /// <summary>
    /// ID.
    /// </summary>
    [Required]
    public int? Id { get; init; }

    /// <summary>
    /// Login.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Login { get; init; }
}