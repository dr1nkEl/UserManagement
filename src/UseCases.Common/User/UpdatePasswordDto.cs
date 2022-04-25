namespace UseCases.Common.User;

/// <summary>
/// Update password DTO.
/// </summary>
public record UpdatePasswordDto
{
    /// <summary>
    /// ID.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Password.
    /// </summary>
    public string Password { get; init; }
}
