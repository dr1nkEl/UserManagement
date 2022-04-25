using Domain;

namespace API.Infrastructure.Models;

/// <summary>
/// User DTO.
/// </summary>
public record UserDto
{
    /// <summary>
    /// ID.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// Gender.
    /// </summary>
    public Gender? Gender { get; init; }

    /// <summary>
    /// Birthday.
    /// </summary>
    public DateTime? BirthDay { get; init; }

    /// <summary>
    /// Date of deletion.
    /// </summary>
    public DateTime? RevokedOn { get; init; }

    /// <summary>
    /// Username.
    /// </summary>
    public string UserName { get; init; }
}
