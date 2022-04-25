using MediatR;

namespace UseCases.Users;

/// <summary>
/// Recover user command.
/// </summary>
/// <param name="Id">ID.</param>
public record RecoverUserCommand(int Id) : IRequest;
