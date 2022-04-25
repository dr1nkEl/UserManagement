namespace UseCases.Common.User;

/// <summary>
/// Update login DTO.
/// </summary>
public record UpdateLoginDto
{
    /// <summary>
    /// ID.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Login.
    /// </summary>
    public string Login { get; init; }
}
