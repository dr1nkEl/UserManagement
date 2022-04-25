using Domain;
using System.ComponentModel.DataAnnotations;

namespace API.Infrastructure.Models;

/// <summary>
/// Patch user model.
/// </summary>
public class PatchUser
{
    /// <summary>
    /// ID of patched user.
    /// </summary>
    [Required]
    public int? Id { get; init; }

    /// <summary>
    /// First name.
    /// </summary>
    [MaxLength(255)]
    [Required]
    public string FirstName { get; init; }

    /// <summary>
    /// Last name.
    /// </summary>
    [MaxLength(255)]
    [Required]
    public string LastName { get; init; }

    /// <summary>
    /// Birthday.
    /// </summary>
    public DateTime? BirthDay { get; init; }

    /// <summary>
    /// Gender.
    /// </summary>
    public Gender? Gender { get; init; }
}
