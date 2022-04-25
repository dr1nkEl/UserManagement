using MediatR;
using UseCases.Common.User;

namespace UseCases.Users;

/// <summary>
/// Get users query.
/// </summary>
public record GetUsersQuery : IRequest<IEnumerable<UserDto>>;
