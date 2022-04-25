using MediatR;
using UseCases.Common.User;

namespace UseCases.Users;

/// <summary>
/// Patch user command.
/// </summary>
/// <param name="Dto">DTO.</param>
public record PatchUserCommand(PatchUserDto Dto) : IRequest;
