using MediatR;
using UseCases.Common.User;

namespace UseCases.Users;

/// <summary>
/// Get users older than given date query.
/// </summary>
/// <param name="Date">Date.</param>
public record GetUsersOlderThanQuery(DateTime Date) : IRequest<IEnumerable<UserDto>>;
