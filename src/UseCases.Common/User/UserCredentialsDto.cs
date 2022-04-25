namespace UseCases.Common.User;

/// <summary>
/// User credentials DTO.
/// </summary>
public record UserCredentialsDto
{
    /// <summary>
    /// Login.
    /// </summary>
    public string Login { get; init; }

    /// <summary>
    /// Password.
    /// </summary>
    public string Password { get; init; }
}
