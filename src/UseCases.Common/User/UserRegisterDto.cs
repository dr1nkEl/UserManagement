using Domain;

namespace UseCases.Common.User;

public record UserRegisterDto
{
    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// Login.
    /// </summary>
    public string Login { get; init; }

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
