using System.ComponentModel.DataAnnotations;

namespace API.Infrastructure.Models;

/// <summary>
/// Add user to role model.
/// </summary>
public record AddUserToRole
{
    /// <summary>
    /// User ID.
    /// </summary>
    [Required]
    public int? UserId { get; init; }

    /// <summary>
    /// Role ID.
    /// </summary>
    [Required]
    public int? RoleId { get; init; }
}
