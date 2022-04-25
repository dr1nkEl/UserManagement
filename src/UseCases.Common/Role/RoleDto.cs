namespace UseCases.Common.Role;

/// <summary>
/// Role DTO.
/// </summary>
public record RoleDto
{
    /// <summary>
    /// ID of role.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Name of role.
    /// </summary>
    public string Name { get; init; }
}
