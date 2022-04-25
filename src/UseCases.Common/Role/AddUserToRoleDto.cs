namespace UseCases.Common.Role;

/// <summary>
/// Add user to role DTO.
/// </summary>
public record AddUserToRoleDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public int UserId { get; init; }

    /// <summary>
    /// Role ID.
    /// </summary>
    public int RoleId { get; init; }
}
