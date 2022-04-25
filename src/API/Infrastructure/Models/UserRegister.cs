using Domain;
using System.ComponentModel.DataAnnotations;

namespace API.Infrastructure.Models;

/// <summary>
/// User registration model.
/// </summary>
public class UserRegister
{
    [Required]
    [MaxLength(255)]
    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; init; }

    [Required]
    [MaxLength(255)]
    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; init; }

    [Required]
    [MaxLength(255)]
    /// <summary>
    /// Login.
    /// </summary>
    public string Login { get; init; }

    [Required]
    [MaxLength(255)]
    /// <summary>
    /// Password.
    /// </summary>
    public string Password { get; init; }

    /// <summary>
    /// Gender.
    /// </summary>
    public Gender? Gender { get; init; }

    /// <summary>
    /// Birthday.
    /// </summary>
    public DateTime? BirthDay { get; init; }
}
