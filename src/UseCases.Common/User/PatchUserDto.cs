using Domain;
using System.ComponentModel.DataAnnotations;

namespace UseCases.Common.User;

/// <summary>
/// Patch user DTO.
/// </summary>
public record PatchUserDto
{
    /// <summary>
    /// ID of patched user.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// First name.
    /// </summary>
    [MaxLength(255)]
    public string FirstName { get; init; }

    /// <summary>
    /// Last name.
    /// </summary>
    [MaxLength(255)]
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
