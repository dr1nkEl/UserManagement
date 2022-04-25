using MediatR;

namespace UseCases.Users;

/// <summary>
/// Delete user command.
/// </summary>
/// <param name="Login">Login.</param>
public record DeleteUserCommand(string Login) : IRequest;
