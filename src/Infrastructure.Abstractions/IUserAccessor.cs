using Domain;

namespace Infrastructure.Abstractions;

/// <summary>
/// Abstraction to access user.
/// </summary>
public interface IUserAccessor
{
    /// <summary>
    /// Get logged user.
    /// </summary>
    /// <returns>User.</returns>
    Task<User> GetMeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if current user can edit user with given ID.
    /// </summary>
    /// <param name="id">ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if can.</returns>
    Task<bool> CanEditAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Set last modified.
    /// </summary>
    /// <param name="id">Id of updated user.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task SetLastModifiedAsync(int id, CancellationToken cancellationToken = default);
}
