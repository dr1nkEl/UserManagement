using MediatR;
using UseCases.Common.User;

namespace UseCases.Users;

/// <summary>
/// Get user query.
/// </summary>
/// <param name="Login">Login.</param>
public record GetUserQuery(string Login) : IRequest<UserDto>;
