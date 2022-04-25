using MediatR;

namespace UseCases.Users;

/// <summary>
/// Revoke user command.
/// </summary>
/// <param name="Id">ID.</param>
public record RevokeUserCommand(int Id) : IRequest;
